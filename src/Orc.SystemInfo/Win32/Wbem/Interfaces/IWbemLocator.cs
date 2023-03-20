namespace Orc.SystemInfo.Win32;

using System;
using System.Runtime.InteropServices;

[ComImport]
[Guid("dc12a687-737f-11cf-884d-00aa004b2e24")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface IWbemLocator
{
    [PreserveSig]
    HResult ConnectServer(
        [In, MarshalAs(UnmanagedType.BStr)]
        string? networkResource,
        [In, MarshalAs(UnmanagedType.BStr)]
        string? userName,
        [In, MarshalAs(UnmanagedType.BStr)]
        string? userPassword,
        [In, MarshalAs(UnmanagedType.BStr)]
        string? locale,
        [In]
        WbemConnectOption wbemConnectOption,
        [In, MarshalAs(UnmanagedType.BStr)]
        string? authority,
        [In, MarshalAs(UnmanagedType.Interface)]
        IWbemContext? ctx,
        [Out, MarshalAs(UnmanagedType.Interface)]
        out IWbemServices wbemServices);
}