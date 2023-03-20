namespace Orc.SystemInfo.Win32;

using System.Runtime.InteropServices;

[ComImport]
[Guid("7c857801-7381-11cf-884d-00aa004b2e24")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface IWbemObjectSink
{
    [PreserveSig]
    HResult Indicate(
        [In]
        int objectCount,
        [In]
        IWbemClassObject[] objArray);

    [PreserveSig]
    HResult SetStatus(
        [In]
        int flags,
        [In]
        HResult hresult,
        [In, MarshalAs(UnmanagedType.BStr)]
        string param,
        [In]
        IWbemClassObject objectParam);
}