namespace Orc.SystemInfo.Win32;

using System.Runtime.InteropServices;

[ComImport]
[Guid("44aca675-e8fc-11d0-a07c-00c04fb68820")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface IWbemCallResult
{
    [PreserveSig]
    HResult GetResultObject([In] int timeout, [Out] out IWbemClassObject resultObject);

    HResult GetResultString([In] int timeout, [Out, MarshalAs(UnmanagedType.BStr)] out string resultString);

    [PreserveSig]
    HResult GetResultServices([In] int timeout, [Out] out IWbemServices services);

    [PreserveSig]
    HResult GetCallStatus([In] int timeout, [Out] out HResult status);
}