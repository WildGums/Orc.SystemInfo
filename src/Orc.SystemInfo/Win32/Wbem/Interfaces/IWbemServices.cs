namespace Orc.SystemInfo.Win32
{
    using System.Runtime.InteropServices;

    [ComImport]
    [Guid("9556dc99-828c-11cf-a37e-00aa003240c7")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IWbemServices
    {
        [PreserveSig]
        HResult OpenNamespace(
               [In, MarshalAs(UnmanagedType.BStr)]
            string targetNamespace,
               [In]
            int flags,
               [In]
            IWbemContext ctx,
               [Out]
            out IWbemServices workingNamespace,
               [Out]
            out IWbemCallResult result);

        [PreserveSig]
        HResult CancelAsyncCall(
            [In]
            IWbemObjectSink sink);

        [PreserveSig]
        HResult QueryObjectSink(
            [In]
            int flags,
            [Out]
            out IWbemObjectSink responseHandler);

        [PreserveSig]
        HResult GetObject(
            [In, MarshalAs(UnmanagedType.BStr)]
            string objectPath,
            [In]
            int flags,
            [In]
            IWbemContext ctx,
            [Out]
            out IWbemClassObject outObject,
            [Out]
            out IWbemCallResult callResult);

        [PreserveSig]
        HResult GetObjectAsync(
            [In, MarshalAs(UnmanagedType.BStr)]
            string objectPath,
            [In]
            int flags,
            [In]
            IWbemContext ctx,
            [In]
            IWbemObjectSink responseHandler);

        [PreserveSig]
        HResult PutClass(
            [In]
            IWbemClassObject wbemClassObject,
            [In]
            int flags,
            [In]
            IWbemContext ctx,
            [Out]
            out IWbemCallResult callResult);

        [PreserveSig]
        HResult PutClassAsync(
            [In]
            IWbemClassObject wbemClassObject,
            [In]
            int flags,
            [In]
            IWbemContext ctx,
            [In]
            IWbemObjectSink responseHandler);

        [PreserveSig]
        HResult DeleteClass(
            [In, MarshalAs(UnmanagedType.BStr)]
            string className,
            [In]
            int flags,
            [In]
            IWbemContext ctx,
            [Out]
            out IWbemCallResult callResult);

        [PreserveSig]
        HResult DeleteClassAsync(
            [In, MarshalAs(UnmanagedType.BStr)]
            string className,
            [In]
            int flags,
            [In]
            IWbemContext ctx,
            [In]
            IWbemObjectSink responseHandler);

        [PreserveSig]
        HResult CreateClassEnum(
            [In, MarshalAs(UnmanagedType.BStr)]
            string superclass,
            [In]
            int flags,
            [In]
            IWbemContext ctx,
            [Out]
            out IWbemClassObjectEnumerator enumerator);

        [PreserveSig]
        HResult CreateClassEnumAsync(
            [In, MarshalAs(UnmanagedType.BStr)]
            string superclass,
            [In]
            int flags,
            [In]
            IWbemContext ctx,
            [In]
            IWbemObjectSink responseHandler);

        [PreserveSig]
        HResult PutInstance(
            [In]
            IWbemClassObject inst,
            [In]
            int flags,
            [In]
            IWbemContext ctx,
            [Out]
            out IWbemCallResult callResult);

        [PreserveSig]
        HResult PutInstanceAsync(
            [In]
            IWbemClassObject inst,
            [In]
            int flags,
            [In]
            IWbemContext ctx,
            [In]
            IWbemObjectSink responseHandler);

        [PreserveSig]
        HResult DeleteInstance(
            [In, MarshalAs(UnmanagedType.BStr)]
            string objectPath,
            [In]
            int flags,
            [In]
            IWbemContext ctx,
            [Out]
            out IWbemCallResult callResult);

        [PreserveSig]
        HResult DeleteInstanceAsync(
            [In, MarshalAs(UnmanagedType.BStr)]
            string objectPath,
            [In]
            int flags,
            [In]
            IWbemContext ctx,
            [In]
            IWbemObjectSink responseHandler);

        [PreserveSig]
        HResult CreateInstanceEnum(
            [In, MarshalAs(UnmanagedType.BStr)]
            string filter,
            [In]
            int flags,
            [In]
            IWbemContext ctx,
            [Out]
            out IWbemClassObjectEnumerator enumerator);

        [PreserveSig]
        HResult CreateInstanceEnumAsync(
            [In, MarshalAs(UnmanagedType.BStr)]
            string filter,
            [In]
            int flags,
            [In]
            IWbemContext ctx,
            [In]
            IWbemObjectSink responseHandler);

        [PreserveSig]
        HResult ExecQuery(
            [In, MarshalAs(UnmanagedType.BStr)]
            string queryLanguage,
            [In, MarshalAs(UnmanagedType.BStr)]
            string query,
            [In]
            WbemClassObjectEnumeratorBehaviorOptions behaviorOption,
            [In]
            IWbemContext? ctx,
            [Out]
            out IWbemClassObjectEnumerator enumerator);

        [PreserveSig]
        HResult ExecQueryAsync(
            [In, MarshalAs(UnmanagedType.BStr)]
            string queryLanguage,
            [In, MarshalAs(UnmanagedType.BStr)]
            string query,
            [In]
            WbemClassObjectEnumeratorBehaviorOptions behaviorOption,
            [In]
            IWbemContext ctx,
            [In]
            IWbemObjectSink responseHandler);

        [PreserveSig]
        HResult ExecNotificationQuery(
            [In, MarshalAs(UnmanagedType.BStr)]
            string queryLanguage,
            [In, MarshalAs(UnmanagedType.BStr)]
            string query,
            [In]
            WbemClassObjectEnumeratorBehaviorOptions behaviorOption,
            [In]
            IWbemContext ctx,
            [Out]
            out IWbemClassObjectEnumerator enumerator);

        [PreserveSig]
        HResult ExecNotificationQueryAsync(
            [In, MarshalAs(UnmanagedType.BStr)]
            string queryLanguage,
            [In, MarshalAs(UnmanagedType.BStr)]
            string query,
            [In]
            WbemClassObjectEnumeratorBehaviorOptions behaviorOption,
            [In]
            IWbemContext ctx,
            [In]
            IWbemObjectSink responseHandler);

        [PreserveSig]
        HResult ExecMethod(
            [In, MarshalAs(UnmanagedType.BStr)]
            string objectPath,
            [In, MarshalAs(UnmanagedType.BStr)]
            string methodName,
            [In]
            int flags,
            [In]
            IWbemContext ctx,
            [In]
            IWbemClassObject inParams,
            [Out]
            out IWbemClassObject outParams,
            [Out]
            out IWbemCallResult callResult);

        [PreserveSig]
        HResult ExecMethodAsync(
            [In, MarshalAs(UnmanagedType.BStr)]
            string objectPath,
            [In, MarshalAs(UnmanagedType.BStr)]
            string methodName,
            [In]
            int flags,
            [In]
            IWbemContext ctx,
            [In]
            IWbemClassObject inParams,
            [In]
            IWbemObjectSink responseHandler);
    }
}
