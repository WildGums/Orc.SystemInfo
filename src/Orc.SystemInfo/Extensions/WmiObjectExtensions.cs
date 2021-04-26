namespace Orc.SystemInfo
{
    using System;
    using Orc.SystemInfo.Wmi;

    public static class WmiObjectExtensions
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
    }
}
