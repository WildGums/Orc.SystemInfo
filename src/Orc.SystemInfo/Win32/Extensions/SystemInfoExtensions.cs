namespace Orc.SystemInfo.Win32;

internal static class SystemInfoExtensions
{
    internal static ulong GetHighestAccessibleMemoryAddress(this Kernel32.SystemInfo systemInfo)
    {
        return (ulong)systemInfo.lpMaximumApplicationAddress.ToInt64();
    }
}