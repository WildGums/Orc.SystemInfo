namespace Orc.SystemInfo.Win32
{
    using System;

    public enum WbemConnectOption
    {
        None = 0x00,
        UseMaxWait = 0x80
    }

    internal enum CimType
    {
        Illegal = 4095,    // 0xFFF
        Empty = 0,    // 0x0
        SInt8 = 16,    // 0x10
        UInt8 = 17,    // 0x11
        SInt16 = 2,    // 0x2
        UInt16 = 18,    // 0x12
        SInt32 = 3,    // 0x3
        UInt32 = 19,    // 0x13
        SInt64 = 20,    // 0x14
        UInt64 = 21,    // 0x15
        Real32 = 4,    // 0x4
        Real64 = 5,    // 0x5
        Boolean = 11,    // 0xB
        String = 8,    // 0x8
        Datetime = 101,    // 0x65
        Reference = 102,    // 0x66
        Char16 = 103,    // 0x67
        Object = 13,    // 0xD
        FlagArray = 8192    // 0x2000
    }

    /// <summary>
    /// Behavior of <see cref="IWbemServices"/> methods which in use of <see cref="IWbemClassObjectEnumerator"/>.
    /// For more information, see <see url="https://docs.microsoft.com/en-us/windows/win32/api/wbemcli/nf-wbemcli-iwbemservices-execquery"/>
    /// </summary>
    [Flags]
    public enum WbemClassObjectEnumeratorBehaviorOptions : int
    {

        /// <summary>
        /// This option causes WMI to retain pointers to objects of the enumeration until the client releases the enumerator.
        /// </summary>
        Bidirectional = 0x00000000,

        /// <summary>
        /// This option is used for prototyping. It does not execute the query and instead returns an object that looks like a typical result object.
        /// </summary>
        Prototype = 0x00000002,

        /// <summary>
        /// This option causes this to be a semisynchronous call.
        /// <para />
        /// For more information, see <see url="http://msdn.microsoft.com/en-us/library/windows/desktop/aa384832(v=vs.85).aspx"/>.
        /// </summary>
        ReturnImmediately = 0x00000010,

        /// <summary>
        /// This flag causes a forward-only enumerator to be returned. 
        /// <para />
        /// Forward-only enumerators are generally much faster and use less memory than conventional enumerators but do not allow calls to 
        /// <see cref="IWbemClassObjectEnumerator.Clone"/> or <see cref="IWbemClassObjectEnumerator.Reset"/>.
        /// </summary>
        ForwardOnly = 0x00000020,

        /// <summary>
        /// This option causes direct access to the provider for the class specified without any regard to its parent class or subclasses.
        /// </summary>
        DirectRead = 0x00000200,

        /// <summary>
        /// This option ensures that any returned objects have enough information in them so that the system properties, such as __PATH, __RELPATH, and __SERVER, are non-NULL.
        /// </summary>
        EnsureLocatable = 0x00000100,

        /// <summary>
        /// If this option is set, WMI retrieves the amended qualifiers stored in the localized namespace of the current connection's locale.
        /// <para />
        /// If not set, only the qualifiers stored in the immediate namespace are retrieved.
        /// </summary>
        UseAmendedQualifiers = 0x00020000
    }

    /// <summary>
    /// Enumeration to specify a condition for the <see cref="IWbemClassObject.GetNames"/>() method. 
    /// For more information, see <see url="https://docs.microsoft.com/en-us/windows/win32/api/wbemcli/nf-wbemcli-iwbemclassobject-getnames"/>
    /// </summary>
    [Flags]
    internal enum WbemConditionFlag
    {
        /// <summary>
        /// Return all property names. <para />
        /// The <see name="IWbemClassObject.GetNames.qualifierName"/> and <see name="IWbemClassObject.GetNames.qualifierValue"/> parameters are not used.
        /// </summary>
        Always = unchecked(0x00000000),

        /// <summary>
        /// Return only properties that have a qualifier of the name specified by the parameter <see name="IWbemClassObject.GetNames.qualifierName"/>. <para />
        /// If this flag is used, you must specify <see name="IWbemClassObject.GetNames.qualifierName"/>.
        /// </summary>
        OnlyIfTrue = unchecked(0x00000001),

        /// <summary>
        /// Return only properties that do not have a qualifier of the name specified by the parameter <see name="IWbemClassObject.GetNames.qualifierName"/>. <para />
        /// If this flag is used, you must specify <see name="IWbemClassObject.GetNames.qualifierName"/>.
        /// </summary>
        OnlyIfFalse = unchecked(0x00000002),

        /// <summary>
        /// Return only properties that have a qualifier of the name specified by the parameter <see name="IWbemClassObject.GetNames.qualifierName"/>, 
        /// and also have a value identical to the value specified by the <see name="IWbemClassObject.GetNames.qualifierValue"/> parameter. <para />
        /// </summary>
        OnlyIfIdentical = unchecked(0x00000003),

        /// <summary>
        /// Return only property names that are object references.
        /// </summary>
        KeysOnly = unchecked(0x00000004),

        /// <summary>
        /// Return only property names that are object references.
        /// </summary>
        RefsOnly = unchecked(0x00000008),

        /// <summary>
        /// Return only property names that belong to the derived-most class. Exclude properties from the parent class or parent classes.
        /// </summary>
        LocalOnly = unchecked(0x00000010),

        /// <summary>
        /// Return only property names that belong to the parent class or parent classes.
        /// </summary>
        PropagnatedOnly = unchecked(0x00000020),

        /// <summary>
        /// Return only system properties.
        /// </summary>
        SystemOnly = unchecked(0x00000030),

        /// <summary>
        /// Return only property names that are not system properties.
        /// </summary>
        NonSystemOnly = unchecked(0x00000040),
    }

    /// <summary>
    /// Options for <see cref="IWbemClassObject"/> object comparison.
    /// For more information, see <see url="https://docs.microsoft.com/en-us/windows/win32/api/wbemcli/ne-wbemcli-wbem_comparison_flag"/>
    /// </summary>
    [Flags]
    internal enum WbemClassObjectComparisonOptions
    {
        /// <summary>
        /// Considered all features.
        /// </summary>
        IncludeAll = 0,

        /// <summary>
        /// Ignore all qualifiers (including Key and Dynamic) in comparison.
        /// </summary>
        IgnoreQualifiers = 1,

        /// <summary>
        /// Ignore the source of the objects, namely the server and the namespace they came from, in comparison to other objects.
        /// </summary>
        IgnoreObjectSource = 2,

        /// <summary>
        /// Ignore default values of properties. This option is only meaningful when comparing classes.
        /// </summary>
        IgnoreDefaultValues = 4,

        /// <summary>
        /// Assume that the objects being compared are instances of the same class. 
        /// Consequently, this option compares instance-related information only.
        /// Use this flag to optimize performance. 
        /// If the objects are not of the same class, the results are undefined.
        /// </summary>
        IgnoreClass = 8,

        /// <summary>
        /// Compare string values in a case-insensitive manner. 
        /// This applies both to strings and to qualifier values. 
        /// Property and qualifier names are always compared in a case-insensitive manner whether this option is specified or not.
        /// </summary>
        IgnoreCase = 16,

        /// <summary>
        /// Ignore qualifier flavors. 
        /// This flag still takes qualifier values into account, but ignores flavor distinctions such as propagation rules and override restrictions (for more information, see <see url="http://msdn.microsoft.com/en-us/library/windows/desktop/aa392900(v=vs.85).aspx"/>).
        /// </summary>
        IgnoreFlavor = 32
    }

    /// <summary>
    /// Describes the authentication level to be used to connect to WMI. This is used for the COM connection to WMI.
    /// </summary>
    internal enum WbemAuthenticationLevel : int
    {

        /// <summary>
        /// The default COM authentication level. WMI uses the default Windows Authentication setting.
        /// </summary>
        Default = 0,

        /// <summary>
        /// No COM authentication.
        /// </summary>
        None = 1,

        /// <summary>
        /// Connect-level COM authentication.
        /// </summary>
        Connect = 2,

        /// <summary>
        /// Call-level COM authentication.
        /// </summary>
        Call = 3,

        /// <summary>
        /// Packet-level COM authentication.
        /// </summary>
        Packet = 4,

        /// <summary>
        /// Packet Integrity-level COM authentication.
        /// </summary>
        PacketIntegrity = 5,

        /// <summary>
        /// Packet Privacy-level COM authentication.
        /// </summary>
        PacketPrivacy = 6,

        /// <summary>
        /// The default COM authentication level. WMI uses the default Windows Authentication setting.
        /// </summary>
        Unchanged = -1
    }

    /// <summary>
    /// Describes the impersonation level to be used to connect to WMI.
    /// </summary>
    internal enum WbemImpersonationLevel : int
    {

        /// <summary>
        /// Default impersonation.
        /// </summary>

        Default = 0,

        /// <summary>
        /// Anonymous COM impersonation level that hides the identity of the caller. Calls to WMI may fail with this impersonation level.
        /// </summary>

        Anonymous = 1,

        /// <summary>
        /// Identify-level COM impersonation level that allows objects to query the credentials of the caller. Calls to WMI may fail with this impersonation level.
        /// </summary>

        Identify = 2,

        /// <summary>
        /// Impersonate-level COM impersonation level that allows objects to use the credentials of the caller. This is the recommended impersonation level for WMI calls.
        /// </summary>

        Impersonate = 3,

        /// <summary>
        /// Delegate-level COM impersonation level that allows objects to permit other objects to use the credentials of the caller. 
        /// This level, which will work with WMI calls but may constitute an unnecessary security risk, is supported only under Windows 2000.
        /// </summary>

        Delegate = 4
    }

    /// <summary>
    /// Contains constants used to distinguish between classes and instances.
    /// </summary>
    public enum WmiObjectGenus
    {
        /// <summary>
        /// Indicates class. 
        /// </summary>
        Class = 1,

        /// <summary>
        /// Indicates instance. 
        /// </summary>
        Instance = 2
    }
}

