namespace Orc.SystemInfo.Win32;

using System;
using System.Runtime.InteropServices;

[ComImport]
[Guid("44aca674-e8fc-11d0-a07c-00c04fb68820")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface IWbemContext
{
    [PreserveSig]
    HResult Clone(
        [Out]
        out IWbemContext newCopy);

    [PreserveSig]
    HResult GetNames(
        [In]
        uint flags,
        [Out, MarshalAs(UnmanagedType.SafeArray)]
        out IntPtr names);

    [PreserveSig]
    HResult BeginEnumeration(
        [In] uint flags);

    [PreserveSig]
    HResult Next(
        [In]
        uint flags,
        [Out, MarshalAs(UnmanagedType.BStr)]
        out string? name,
        [Out]
        out object? value);

    [PreserveSig]
    HResult EndEnumeration();

    [PreserveSig]
    HResult SetValue(
        [In, MarshalAs(UnmanagedType.LPWStr)]
        string name,
        [In]
        uint flags,
        [In]
        object? value);

    [PreserveSig]
    HResult GetValue(
        [In, MarshalAs(UnmanagedType.LPWStr)]
        string name,
        [In]
        uint flags,
        [Out]
        out object? value);

    [PreserveSig]
    HResult DeleteValue(
        [In, MarshalAs(UnmanagedType.LPWStr)]
        string name,
        [In]
        uint flags);

    [PreserveSig]
    HResult DeleteAll();
}
