namespace Orc.SystemInfo
{
    using System;
    using System.Collections.Generic;
    using Orc.SystemInfo.Wmi;

    public static class WindowsManagementObjectExtensions
    {
        public static string GetValue(this WindowsManagementObject obj, string key, string defaultValue = null)
        {
            try
            {
                return obj.GetValue<string>(key) ?? defaultValue;
            }
            catch (Exception)
            {
                // ignore
                return defaultValue;
            }
        }

        public static string GetValue<TValue>(this WindowsManagementObject obj, string key, string defaultValue = null)
        {
            try
            {
                var result = obj.GetValue<TValue>(key);
                if (EqualityComparer<TValue>.Default.Equals(result, default(TValue)))
                {
                    return defaultValue;
                }
                return Convert.ToString(result) ?? defaultValue;
            }
            catch (Exception)
            {
                // ignore
                return defaultValue;
            }
        }
    }
}
