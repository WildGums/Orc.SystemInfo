namespace Orc.SystemInfo.Win32
{
    using System.Runtime.InteropServices;

    [ComImport]
    [Guid("dc12a681-737f-11cf-884d-00aa004b2e24")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IWbemClassObject
    {
        [PreserveSig]
        HResult GetQualifierSet([Out] out IWbemQualifierSet qualifierSet);

        [PreserveSig]
        HResult Get([In, MarshalAs(UnmanagedType.LPWStr)] string propertyName, [In] int flags, [Out] out object? value, [Out] out CimType type, [Out] out int flavor);

        [PreserveSig]
        HResult Put([In, MarshalAs(UnmanagedType.LPWStr)] string propertyName, [In] int flags, [In] object? value, [In] CimType type);

        [PreserveSig]
        HResult Delete([In, MarshalAs(UnmanagedType.LPWStr)] string propertyName);

        [PreserveSig]
        HResult GetNames([In, MarshalAs(UnmanagedType.LPWStr)] string? qualifierName, [In] WbemConditionFlag flags,
            [In] ref object? qualifierValue, [Out, MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] names);

        [PreserveSig]
        HResult BeginEnumeration([In] int enumFlags);

        [PreserveSig]
        HResult Next([In] int flags, [Out, MarshalAs(UnmanagedType.BStr)] string strName, [Out] out object? value, [Out] out CimType type, [Out] out int flavor);

        HResult EndEnumeration();

        [PreserveSig]
        HResult GetPropertyQualifierSet([In, MarshalAs(UnmanagedType.LPWStr)] string property, [Out] out IWbemQualifierSet qualifierSet);

        [PreserveSig]
        HResult Clone([Out] out IWbemClassObject copy);

        [PreserveSig]
        HResult GetObjectText([In] int flags, [Out, MarshalAs(UnmanagedType.BStr)] out string objectText);

        [PreserveSig]
        HResult SpawnDerivedClass([In] int flags, [Out] out IWbemClassObject newClass);

        [PreserveSig]
        HResult SpawnInstance([In] int flags, [Out] out IWbemClassObject newInstance);

        [PreserveSig]
        HResult CompareTo([In] WbemClassObjectComparisonOptions compareOption, [In] IWbemClassObject compareTo);

        [PreserveSig]
        HResult GetPropertyOrigin([In, MarshalAs(UnmanagedType.LPWStr)] string name, [Out, MarshalAs(UnmanagedType.BStr)] out string className);

        [PreserveSig]
        HResult InheritsFrom([In, MarshalAs(UnmanagedType.LPWStr)] string ancestor);

        [PreserveSig]
        HResult GetMethod([In, MarshalAs(UnmanagedType.LPWStr)] string name, [In] int flags, [Out] out IWbemClassObject inSignature, [Out] out IWbemClassObject outSignature);

        [PreserveSig]
        HResult PutMethod([In, MarshalAs(UnmanagedType.LPWStr)] string name, [In] int flags, [In] IWbemClassObject inSignature, [In] IWbemClassObject outSignature);

        [PreserveSig]
        HResult DeleteMethod([In, MarshalAs(UnmanagedType.LPWStr)] string name);

        [PreserveSig]
        HResult BeginMethodEnumeration([In] int enumFlags);

        [PreserveSig]
        HResult NextMethod([In] int flags, [Out, MarshalAs(UnmanagedType.BStr)] string name, [Out] out IWbemClassObject inSignature, [Out] out IWbemClassObject outSignature);

        [PreserveSig]
        HResult EndMethodEnumeration();

        [PreserveSig]
        HResult GetMethodQualifierSet([In, MarshalAs(UnmanagedType.LPWStr)] string method, [Out] out IWbemQualifierSet qualifierSet);

        [PreserveSig]
        HResult GetMethodOrigin([In, MarshalAs(UnmanagedType.LPWStr)] string methodName, [Out, MarshalAs(UnmanagedType.BStr)] out string className);
    }
}
