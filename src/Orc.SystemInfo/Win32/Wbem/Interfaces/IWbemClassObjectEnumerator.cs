namespace Orc.SystemInfo.Win32;

using System.Runtime.InteropServices;

[ComImport]
[Guid("027947e1-d731-11ce-a357-000000000001")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[TypeLibType(TypeLibTypeFlags.FRestricted)]
internal interface IWbemClassObjectEnumerator
{
    [PreserveSig]
    HResult Reset();

    [PreserveSig]
    HResult Next(
        [In]
        int timeOut,
        [In]
        uint count,
        [Out]
        out IWbemClassObject? objects,
        [Out]
        out uint returnedCount);

    [PreserveSig]
    HResult NextAsync(
        [In] uint count,
        [In] IWbemObjectSink sink);

    [PreserveSig]
    HResult Clone(
        [Out]
        out IWbemClassObjectEnumerator enumerator);

    [PreserveSig]
    HResult Skip(
        [In]
        int timeOut,
        [In]
        uint count);
}
