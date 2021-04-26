namespace Orc.SystemInfo.Win32
{
    using System;
    using System.Runtime.InteropServices;

    internal interface IWbemQualifierSet
    {
        [PreserveSig]
        HResult BeginEnumeration(
            [In]
            int flags);

        [PreserveSig]
        HResult Delete(
            [In, MarshalAs(UnmanagedType.LPWStr)]
            string name);

        [PreserveSig]
        HResult EndEnumeration();


        [PreserveSig]
        HResult Get(
            [In, MarshalAs(UnmanagedType.LPWStr)]
            string name,
            [In]
            int flags,
            [Out]
            out object value,
            [Out]
            out int flavor);

        [PreserveSig]
        HResult GetNames(
            [In]
            int flags,
            [Out, MarshalAs(UnmanagedType.SafeArray)]
            out IntPtr names);

        [PreserveSig]
        HResult Next(
            [In]
            int flags,
            [Out, MarshalAs(UnmanagedType.BStr)]
            out string name,
            [Out]
            out object value,
            [Out]
            out int flavor);

        [PreserveSig]
        HResult Put(
            [In, MarshalAs(UnmanagedType.LPWStr)]
            string name,
            [In]
            object value,
            [In]
            int flavor);
    }
}
