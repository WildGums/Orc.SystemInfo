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

        public static string GetValue<TResult>(this WindowsManagementObject obj, string key, string defaultValue = null)
        {
            try
            {
                var result = obj.GetValue<TResult>(key);
                if (EqualityComparer<TResult>.Default.Equals(result, default(TResult)))
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
