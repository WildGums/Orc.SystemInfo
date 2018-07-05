// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WmiService.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.SystemInfo
{
    using System;
    using System.Management;

    public class WindowsManagementInformationService : IWindowsManagementInformationService
    {
        public string GetIdentifier(string wmiClass, string wmiProperty)
        {
            return GetIdentifier(wmiClass, wmiProperty, null, null);
        }

        public string GetIdentifier(string wmiClass, string wmiProperty, string additionalWmiToCheck, string additionalWmiToCheckValue)
        {
            var result = string.Empty;

            var query = string.Format("SELECT {0}{1} FROM {2}", wmiProperty,
                string.IsNullOrWhiteSpace(additionalWmiToCheck) ? string.Empty : string.Format(", {0}", additionalWmiToCheck), wmiClass);
            var wmiSearcher = new ManagementObjectSearcher(query);
            var managementObjectCollection = wmiSearcher.Get();

            foreach (var managementObject in managementObjectCollection)
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(additionalWmiToCheck))
                    {
                        var wmiToCheckValue = managementObject.GetValue(additionalWmiToCheck);

                        var wmiToCheckValueValue = additionalWmiToCheckValue;
                        var invert = additionalWmiToCheckValue.StartsWith("!");
                        if (invert)
                        {
                            wmiToCheckValueValue = additionalWmiToCheckValue.Substring(1);
                        }

                        var equals = string.Equals(wmiToCheckValue, wmiToCheckValueValue, StringComparison.OrdinalIgnoreCase);
                        if ((!equals && !invert) || (equals && invert))
                        {
                            continue;
                        }
                    }

                    var value = managementObject.GetValue(wmiProperty);
                    if (value != null)
                    {
                        result = value;
                        break;
                    }
                }
                catch (Exception)
                {
                }
            }

            return result;
        }
    }
}
