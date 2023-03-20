namespace Orc.SystemInfo.Win32;

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[ComImport]
[Guid("4590f811-1d3a-11d0-891f-00aa004b2e24")]
internal class WbemLocator : IWbemLocator
{
    [PreserveSig]
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public virtual extern HResult ConnectServer(string? networkResource, string? userName, string? userPassword, string? locale, WbemConnectOption wbemConnectOption, string? authority, IWbemContext? ctx, out IWbemServices wbemServices);
}