namespace Orc.SystemInfo
{
    using System;
    using System.Runtime.InteropServices;
    using Catel.Logging;
    using Orc.SystemInfo.Wmi;

    public class WindowsManagementInformationService : IWindowsManagementInformationService
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public string GetIdentifier(string wmiClass, string wmiProperty)
        {
            return GetIdentifier(wmiClass, wmiProperty, null, null);
        }

        public string GetIdentifier(string wmiClass, string wmiProperty, string? additionalWmiToCheck, string? additionalWmiToCheckValue)
        {
            try
            {
                var result = string.Empty;

                var query = string.Format("SELECT {0}{1} FROM {2}", wmiProperty,
                    string.IsNullOrWhiteSpace(additionalWmiToCheck) ? string.Empty : string.Format(", {0}", additionalWmiToCheck), wmiClass);

                using (var connection = new WindowsManagementConnection())
                {
                    foreach (var managementObject in connection.CreateQuery(query))
                    {
                        if (managementObject is null)
                        {
                            continue;
                        }

                        try
                        {
                            if (!string.IsNullOrWhiteSpace(additionalWmiToCheck) && 
                                !string.IsNullOrWhiteSpace(additionalWmiToCheckValue))
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
                            return result;
                        }
                    }
                }

                return result;
            }
            catch (COMException)
            {
                return string.Empty;
            }
        }
    }
}
