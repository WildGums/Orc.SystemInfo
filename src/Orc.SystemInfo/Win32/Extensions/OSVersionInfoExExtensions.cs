namespace Orc.SystemInfo.Win32
{
    using System;

    internal static class OSVersionInfoExExtensions
    {
        internal static Version GetOSVersion(this Kernel32.OSVersionInfoEx versionInfo)
        {
            return new Version((int)versionInfo.dwMajorVersion, (int)versionInfo.dwMinorVersion, (int)versionInfo.dwBuildNumber);
        }
    }
}
