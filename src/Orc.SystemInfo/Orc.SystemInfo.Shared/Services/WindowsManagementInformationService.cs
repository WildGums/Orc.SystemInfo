// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WmiService.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.SystemInfo
{
    using System;
    using System.Management;
    using Catel.Logging;

    public class WindowsManagementInformationService : IWindowsManagementInformationService
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public string GetIdentifier(string wmiClass, string wmiProperty)
        {
            return GetIdentifier(wmiClass, wmiProperty, null, null);
        }

        public string GetIdentifier(string wmiClass, string wmiProperty, string additionalWmiToCheck, string additionalWmiToCheckValue)
        {
            var result = string.Empty;

            var managementClass = new ManagementClass(wmiClass);
            var managementObjectCollection = managementClass.GetInstances();
            foreach (var managementObject in managementObjectCollection)
            {
                // Only get the first one
                if (string.IsNullOrEmpty(result))
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

                            var equals = string.Equals(wmiToCheckValue.ToString(), wmiToCheckValueValue, StringComparison.OrdinalIgnoreCase);
                            if ((!equals && !invert) || (equals && invert))
                            {
                                Log.Debug("Cannot use mgmt object '{0}', wmi property '{1}' is '{2}' but expected '{3}'", wmiClass,
                                    additionalWmiToCheck, wmiToCheckValue, additionalWmiToCheckValue);
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
                    catch (Exception ex)
                    {
                        Log.Debug(ex, "Failed to retrieve object '{0}.{1}', additional wmi to check: {2}", wmiClass, wmiProperty, additionalWmiToCheck);
                    }
                }
            }

            return result;
        }
    }
}