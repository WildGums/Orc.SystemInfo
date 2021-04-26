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

        /// <summary>
        /// Invalid Type.
        /// </summary>

        None = 0,

        /// <summary>
        /// A signed 16-bit integer.
        /// </summary>

        SInt16 = 2,

        /// <summary>
        /// A signed 32-bit integer.
        /// </summary>

        SInt32 = 3,

        /// <summary>
        /// A floating-point 32-bit number.
        /// </summary>

        Real32 = 4,

        /// <summary>
        /// A floating point 64-bit number.
        /// </summary>

        Real64 = 5,

        /// <summary>
        /// A string.
        /// </summary>

        String = 8,

        /// <summary>
        /// A boolean.
        /// </summary>

        Boolean = 11,

        /// <summary>
        /// An embedded object.
        /// <para />
        /// Note that embedded objects differ from references in that the embedded object doesn't have a path and its lifetime is identical to the lifetime of the containing object.
        /// </summary>

        Object = 13,

        /// <summary>
        /// A signed 8-bit integer.
        /// </summary>

        SInt8 = 16,

        /// <summary>
        /// An unsigned 8-bit integer.
        /// </summary>

        UInt8 = 17,

        /// <summary>
        /// An unsigned 16-bit integer.
        /// </summary>

        UInt16 = 18,

        /// <summary>
        /// An unsigned 32-bit integer.
        /// </summary>

        UInt32 = 19,

        /// <summary>
        /// A signed 64-bit integer.
        /// </summary>

        SInt64 = 20,

        /// <summary>
        /// An unsigned 64-bit integer.
        /// </summary>

        UInt64 = 21,

        /// <summary>
        /// A date or time value, represented in a string in DMTF date/time format: yyyymmddHHMMSS.mmmmmmsUUU
        /// <para>where:</para>
        /// <para>yyyymmdd - is the date in year/month/day</para>
        /// <para>HHMMSS - is the time in hours/minutes/seconds</para>
        /// <para>mmmmmm - is the number of microseconds in 6 digits</para>
        /// <para>sUUU - is a sign (+ or -) and a 3-digit UTC offset</para>
        /// </summary>

        DateTime = 101,

        /// <summary>
        /// A reference to another object. This is represented by a string containing the path to the referenced object.
        /// </summary>

        Reference = 102,

        /// <summary>
        /// A 16-bit character.
        /// </summary>

        Char16 = 103,
    }

    [Flags]
    internal enum WbemClassObjectEnumeratorBehaviorOption : int
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
    /// Describes the possible CIM types for properties, qualifiers, or method parameters.
    /// </summary>
    /// <summary>
    /// Enumeration to specify a condition for the <see cref="IWbemClassObject.GetNames"/>() method. 
    /// </summary>
    internal enum WbemConditionFlag
    {

        /// <summary>
        /// Return all property names. <para />
        /// The <see name="IWbemClassObject.GetNames.qualifierName"/> and <see name="IWbemClassObject.GetNames.qualifierValue"/> parameters are not used.
        /// </summary>

        WBEM_FLAG_ALWAYS = unchecked((int)0x00000000),

        /// <summary>
        /// Return only properties that have a qualifier of the name specified by the parameter <see name="IWbemClassObject.GetNames.qualifierName"/>. <para />
        /// If this flag is used, you must specify <see name="IWbemClassObject.GetNames.qualifierName"/>.
        /// </summary>

        WBEM_FLAG_ONLY_IF_TRUE = unchecked((int)0x00000001),

        /// <summary>
        /// Return only properties that do not have a qualifier of the name specified by the parameter <see name="IWbemClassObject.GetNames.qualifierName"/>. <para />
        /// If this flag is used, you must specify <see name="IWbemClassObject.GetNames.qualifierName"/>.
        /// </summary>

        WBEM_FLAG_ONLY_IF_FALSE = unchecked((int)0x00000002),

        /// <summary>
        /// Return only properties that have a qualifier of the name specified by the parameter <see name="IWbemClassObject.GetNames.qualifierName"/>, 
        /// and also have a value identical to the value specified by the <see name="IWbemClassObject.GetNames.qualifierValue"/> parameter. <para />
        /// </summary>

        WBEM_FLAG_ONLY_IF_IDENTICAL = unchecked((int)0x00000003),

        /// <summary>
        /// Return only property names that are object references.
        /// </summary>

        WBEM_FLAG_KEYS_ONLY = unchecked((int)0x00000004),

        /// <summary>
        /// Return only property names that are object references.
        /// </summary>

        WBEM_FLAG_REFS_ONLY = unchecked((int)0x00000008),

        /// <summary>
        /// Return only property names that belong to the derived-most class. Exclude properties from the parent class or parent classes.
        /// </summary>

        WBEM_FLAG_LOCAL_ONLY = unchecked((int)0x00000010),

        /// <summary>
        /// Return only property names that belong to the parent class or parent classes.
        /// </summary>

        WBEM_FLAG_PROPAGATED_ONLY = unchecked((int)0x00000020),

        /// <summary>
        /// Return only system properties.
        /// </summary>

        WBEM_FLAG_SYSTEM_ONLY = unchecked((int)0x00000030),

        /// <summary>
        /// Return only property names that are not system properties.
        /// </summary>

        WBEM_FLAG_NONSYSTEM_ONLY = unchecked((int)0x00000040),

    }

    #region Description
    /// <summary>
    /// Options for <see cref="IWbemClassObject"/> object comparison.
    /// </summary>
    #endregion
    [Flags]
    internal enum WbemClassObjectComparisonOption
    {
        #region Description
        /// <summary>
        /// Considered all features.
        /// </summary>
        #endregion
        IncludeAll = 0,
        #region Description
        /// <summary>
        /// Ignore all qualifiers (including Key and Dynamic) in comparison.
        /// </summary>
        #endregion
        IgnoreQualifiers = 1,
        #region Description
        /// <summary>
        /// Ignore the source of the objects, namely the server and the namespace they came from, in comparison to other objects.
        /// </summary>
        #endregion
        IgnoreObjectSource = 2,
        #region Description
        /// <summary>
        /// Ignore default values of properties. This option is only meaningful when comparing classes.
        /// </summary>
        #endregion
        IgnoreDefaultValues = 4,
        #region Description
        /// <summary>
        /// Assume that the objects being compared are instances of the same class. 
        /// Consequently, this option compares instance-related information only.
        /// Use this flag to optimize performance. 
        /// If the objects are not of the same class, the results are undefined.
        /// </summary>
        #endregion
        IgnoreClass = 8,
        #region Description
        /// <summary>
        /// Compare string values in a case-insensitive manner. 
        /// This applies both to strings and to qualifier values. 
        /// Property and qualifier names are always compared in a case-insensitive manner whether this option is specified or not.
        /// </summary>
        #endregion
        IgnoreCase = 16,
        #region Description
        /// <summary>
        /// Ignore qualifier flavors. 
        /// This flag still takes qualifier values into account, but ignores flavor distinctions such as propagation rules and override restrictions (for more information, see <see url="http://msdn.microsoft.com/en-us/library/windows/desktop/aa392900(v=vs.85).aspx"/>).
        /// </summary>
        #endregion
        IgnoreFlavor = 32
    }

    #region Description
    /// <summary>
    /// Describes the authentication level to be used to connect to WMI. This is used for the COM connection to WMI.
    /// </summary>
    #endregion
    // RPC AuthenticationLevel
    internal enum AuthenticationLevel : int
    {
        #region Description
        /// <summary>
        /// The default COM authentication level. WMI uses the default Windows Authentication setting.
        /// </summary>
        #endregion
        Default = 0,
        #region Description
        /// <summary>
        /// No COM authentication.
        /// </summary>
        #endregion
        None = 1,
        #region Description
        /// <summary>
        /// Connect-level COM authentication.
        /// </summary>
        #endregion
        Connect = 2,
        #region Description
        /// <summary>
        /// Call-level COM authentication.
        /// </summary>
        #endregion
        Call = 3,
        #region Description
        /// <summary>
        /// Packet-level COM authentication.
        /// </summary>
        #endregion
        Packet = 4,
        #region Description
        /// <summary>
        /// Packet Integrity-level COM authentication.
        /// </summary>
        #endregion
        PacketIntegrity = 5,
        #region Description
        /// <summary>
        /// Packet Privacy-level COM authentication.
        /// </summary>
        #endregion
        PacketPrivacy = 6,
        #region Description
        /// <summary>
        /// The default COM authentication level. WMI uses the default Windows Authentication setting.
        /// </summary>
        #endregion
        Unchanged = -1
    }

    #region Description
    /// <summary>
    /// Describes the impersonation level to be used to connect to WMI.
    /// </summary>
    #endregion
    internal enum ImpersonationLevel : int
    {
        #region Description
        /// <summary>
        /// Default impersonation.
        /// </summary>
        #endregion
        Default = 0,
        #region Description
        /// <summary>
        /// Anonymous COM impersonation level that hides the identity of the caller. Calls to WMI may fail with this impersonation level.
        /// </summary>
        #endregion
        Anonymous = 1,
        #region Description
        /// <summary>
        /// Identify-level COM impersonation level that allows objects to query the credentials of the caller. Calls to WMI may fail with this impersonation level.
        /// </summary>
        #endregion
        Identify = 2,
        #region Description
        /// <summary>
        /// Impersonate-level COM impersonation level that allows objects to use the credentials of the caller. This is the recommended impersonation level for WMI calls.
        /// </summary>
        #endregion
        Impersonate = 3,
        #region Description
        /// <summary>
        /// Delegate-level COM impersonation level that allows objects to permit other objects to use the credentials of the caller. 
        /// This level, which will work with WMI calls but may constitute an unnecessary security risk, is supported only under Windows 2000.
        /// </summary>
        #endregion
        Delegate = 4
    }

    #region Description
    /// <summary>
    /// Enumeration that is used to distinguish <see cref="WmiObject"/>s between classes and instances. 
    /// </summary>
    #endregion
    public enum WmiObjectGenus
    {
        #region Description
        /// <summary>
        /// Indicates that the <see cref="WmiObject"/> is an class. 
        /// </summary>
        #endregion
        Class = 1,

        #region Description
        /// <summary>
        /// Indicates that the <see cref="WmiObject"/> is an instance or an event. 
        /// </summary>
        #endregion
        Instance = 2
    }

    [Flags]
    public enum EnumeratorBehaviorOption
    {
        #region Description
        /// <summary>
        /// This option causes WMI to retain pointers to objects of the enumeration until the client releases the enumerator.
        /// </summary>
        #endregion
        Bidirectional = WbemClassObjectEnumeratorBehaviorOption.Bidirectional,

        #region Description
        /// <summary>
        /// This option is used for prototyping. It does not execute the query and instead returns an object that looks like a typical result object.
        /// </summary>
        #endregion
        Prototype = WbemClassObjectEnumeratorBehaviorOption.Prototype,

        #region Description
        /// <summary>
        /// This option causes this to be a semisynchronous call.
        /// <para />
        /// For more information, see <see url="http://msdn.microsoft.com/en-us/library/windows/desktop/aa384832(v=vs.85).aspx"/>.
        /// </summary>
        #endregion
        ReturnImmediately = WbemClassObjectEnumeratorBehaviorOption.ReturnImmediately,

        #region Description
        /// <summary>
        /// This flag causes a forward-only enumerator to be returned.
        /// <para />
        /// Forward-only enumerators are generally much faster and use less memory than conventional enumerators but do not allow calls to clone or reset the enumerator.
        /// </summary>
        #endregion
        ForwardOnly = WbemClassObjectEnumeratorBehaviorOption.ForwardOnly,

        #region Description
        /// <summary>
        /// This option causes direct access to the provider for the class specified without any regard to its parent class or subclasses.
        /// </summary>
        #endregion
        DirectRead = WbemClassObjectEnumeratorBehaviorOption.DirectRead,

        #region Description
        /// <summary>
        /// This option ensures that any returned objects have enough information in them so that the system properties, such as __PATH, __RELPATH, and __SERVER, are non-NULL.
        /// </summary>
        #endregion
        EnsureLocatable = WbemClassObjectEnumeratorBehaviorOption.EnsureLocatable,

        #region Description
        /// <summary>
        /// If this option is set, WMI retrieves the amended qualifiers stored in the localized namespace of the current connection's locale.
        /// <para />
        /// If not set, only the qualifiers stored in the immediate namespace are retrieved.
        /// </summary>
        #endregion
        UseAmendedQualifiers = WbemClassObjectEnumeratorBehaviorOption.UseAmendedQualifiers
    }
}

