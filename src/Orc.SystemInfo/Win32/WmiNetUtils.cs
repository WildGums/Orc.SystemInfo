namespace Orc.SystemInfo.Win32
{
    using System;
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
        /// Sets the authentication information that will be used to make calls on the specified proxy.
        /// </summary>
        internal static readonly CoSetProxyBlanketForIWbemServicesFunction CoSetProxyBlanketForIWbemServices;

        static WmiNetUtils()
        {
            // string dllPath = @"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\\wminet_utils.dll";
            string dllPath = @"C:\Dev\Orc.SystemInfo\src\Orc.SystemInfo\bin\WbemShim.dll";

            IntPtr loadLibrary = Kernel32.LoadLibrary(dllPath);

            if (loadLibrary != IntPtr.Zero)
            {
                IntPtr procAddr = Kernel32.GetProcAddress(loadLibrary, "BlessIWbemServices");

                CoSetProxyBlanketForIWbemServices = (CoSetProxyBlanketForIWbemServicesFunction)Marshal.GetDelegateForFunctionPointer(procAddr, typeof(CoSetProxyBlanketForIWbemServicesFunction));
            }
            else
            {
                var error = Marshal.GetLastWin32Error();
                throw new COMException($"Error calling native library", error);
            }
        }

        /// <summary>
        /// Gets pointer to function setting proxy authorization on wbem service
        /// </summary>
        /// <param name="wbemServices"></param>
        /// <param name="impersonationLevel"></param>
        /// <param name="authenticationLevel"></param>
        /// <returns></returns>
        internal delegate HResult CoSetProxyBlanketForIWbemServicesFunction(
            [In, MarshalAs(UnmanagedType.Interface)]
            IWbemServices wbemServices,
            [In]
            ImpersonationLevel impersonationLevel,
            [In]
            AuthenticationLevel authenticationLevel);
    }
}
