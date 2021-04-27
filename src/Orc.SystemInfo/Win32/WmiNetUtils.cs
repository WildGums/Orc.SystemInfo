namespace Orc.SystemInfo.Win32
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
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
            var loadLibrary = LoadDlls();

            if (loadLibrary != IntPtr.Zero)
            {
                var procAddr = Kernel32.GetProcAddress(loadLibrary, "BlessIWbemServices");

                CoSetProxyBlanketForIWbemServices = (CoSetProxyBlanketForIWbemServicesFunction)Marshal.GetDelegateForFunctionPointer(procAddr, typeof(CoSetProxyBlanketForIWbemServicesFunction));
            }
            else
            {
                var error = Marshal.GetLastWin32Error();
                throw new COMException($"Error calling native library", error);
            }
        }

        private static IntPtr LoadDlls()
        {
            var fileNames = ExtractWbemShimDlls();
            foreach (var fileName in fileNames)
            {
                var pointer = Kernel32.LoadLibrary(fileName);
                if (pointer != IntPtr.Zero)
                {
                    return pointer;
                }
            }

            return IntPtr.Zero;
        }

        private static IEnumerable<string> ExtractWbemShimDlls()
        {
            var dirName = Path.Combine(Path.GetTempPath(), "Orc.SystemInfo." +
                Assembly.GetExecutingAssembly().GetName().Version.ToString());

            if (!Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
            }

            var dlls = new[]
            {
                "x64.WbemShim.dll",
                "x86.WbemShim.dll",
            };

            foreach (var dll in dlls)
            {
                using var readStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Orc.SystemInfo.Resources.Dlls." + dll);
                var dllPath = Path.Combine(dirName, dll);

                // Copy the assembly to the temporary file
                try
                {
                    using var writeStream = File.Create(dllPath);

                    const int sz = 4096;
                    var buf = new byte[sz];
                    while (true)
                    {
                        var nRead = readStream.Read(buf, 0, sz);
                        if (nRead < 1)
                            break;
                        writeStream.Write(buf, 0, nRead);
                    }
                }
                catch
                {
                    // This may happen if another process has already created and loaded the file.
                    // Since the directory includes the version number of this assembly we can
                    // assume that it's the same bits, so we just ignore the excecption here and
                    // load the DLL.

                    dllPath = null;
                }

                if (dllPath is not null)
                {
                    yield return dllPath;
                }
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
            WbemImpersonationLevel impersonationLevel,
            [In]
            WbemAuthenticationLevel authenticationLevel);
    }
}
