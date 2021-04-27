// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WmiService.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.SystemInfo
{
    using System;
    using Orc.SystemInfo.Wmi;

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

            using (WindowsManagementConnection connection = new WindowsManagementConnection())
            {
                foreach (var managementObject in connection.CreateQuery(query))
                {
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(additionalWmiToCheck))
                        {
                            var wmiToCheckValue = managementObject.GetValue(additionalWmiToCheck, Convert.ToString);

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

                        var value = managementObject.GetValue(wmiProperty, Convert.ToString);
                        if (value is not null)
                        {
                            result = value;
                            break;
                        }
                    }
                    catch (Exception)
                    {
                        // Ignore
                    }
                }
            }

            return result;
        }
    }
}
