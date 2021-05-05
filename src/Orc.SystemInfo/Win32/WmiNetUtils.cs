namespace Orc.SystemInfo.Win32
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// This class wraps calls of CoSetProxyBlanket from managed shim library.
    /// CoSetProxyBlanket and CoInitializeSecurity both work to set the permissions context in which the process invoked immediately after is executed.
    /// Calling them from within that process is useless because it's too late at that point; the permissions context has already been set.
    /// Specifically, these methods are meant to be called from non-managed code such as a C++ wrapper that then invokes the managed, i.e. C# or VB.NET, code.
    /// For more information, see <see url="https://docs.microsoft.com/en-us/archive/blogs/mbend/cosetproxyblanket-not-supported-from-managed-code"/>
    /// <see url="https://docs.microsoft.com/en-us/windows/win32/wmisdk/setting-the-security-levels-on-a-wmi-connection"/>.
    /// <see url="https://docs.microsoft.com/en-us/windows/win32/wmisdk/setting-client-application-process-security"/>
    /// </summary>
    internal static class WmiNetUtils
    {
        /// <summary>
        /// Gets pointer to function setting proxy authorization on wbem service
        /// </summary>
        /// <param name="wbemServices"></param>
        /// <param name="impersonationLevel"></param>
        /// <param name="authenticationLevel"></param>
        /// <returns></returns>
        [DllImport("WbemShim.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        internal static extern HResult BlessIWbemServices(
            [In, MarshalAs(UnmanagedType.Interface)]
            IWbemServices wbemServices,
            [In]
            WbemImpersonationLevel impersonationLevel,
            [In]
            WbemAuthenticationLevel authenticationLevel);
    }
}
