namespace Orc.SystemInfo.Win32;

using System;

public enum WbemConnectOption
{
    None = 0x00,
    UseMaxWait = 0x80
}

/// <summary>
/// Behavior of WMI query execution methods which use an enumerator.
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
    /// Forward-only enumerators are generally much faster and use less memory than conventional enumerators but do not allow calls to Clone or Reset.
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

[Flags]
public enum LoadLibraryFlags : uint
{
    None = 0,
    DONT_RESOLVE_DLL_REFERENCES = 0x00000001,
    LOAD_IGNORE_CODE_AUTHZ_LEVEL = 0x00000010,
    LOAD_LIBRARY_AS_DATAFILE = 0x00000002,
    LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 0x00000040,
    LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x00000020,
    LOAD_LIBRARY_SEARCH_APPLICATION_DIR = 0x00000200,
    LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000,
    LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR = 0x00000100,
    LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800,
    LOAD_LIBRARY_SEARCH_USER_DIRS = 0x00000400,
    LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008
}
