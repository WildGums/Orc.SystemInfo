[assembly: System.Resources.NeutralResourcesLanguage("en-US")]
[assembly: System.Runtime.InteropServices.ComVisible(false)]
[assembly: System.Runtime.Versioning.TargetFramework(".NETCoreApp,Version=v5.0", FrameworkDisplayName="")]
public static class ModuleInitializer
{
    public static void Initialize() { }
}
namespace Orc.SystemInfo
{
    public class DbProvidersService : Orc.SystemInfo.IDbProvidersService
    {
        public DbProvidersService() { }
        public System.Collections.Generic.IEnumerable<string> GetInstalledDbProviders() { }
    }
    public class DotNetFrameworkService : Orc.SystemInfo.IDotNetFrameworkService
    {
        public DotNetFrameworkService() { }
        protected System.Collections.Generic.IEnumerable<string> BuildFrameworkNamesRecursively(Microsoft.Win32.RegistryKey registryKey, string name, string topLevelSp = "0", bool topLevel = false) { }
        public virtual System.Collections.Generic.IEnumerable<string> GetInstalledFrameworks() { }
        protected System.Collections.Generic.IEnumerable<string> GetNetFrameworkVersions() { }
    }
    public interface IDbProvidersService
    {
        System.Collections.Generic.IEnumerable<string> GetInstalledDbProviders();
    }
    public interface IDotNetFrameworkService
    {
        System.Collections.Generic.IEnumerable<string> GetInstalledFrameworks();
    }
    public interface ISystemIdentificationService
    {
        string GetCpuId();
        string GetGpuId();
        string GetHardDriveId();
        string GetMacId();
        string GetMachineId(string separator = "-", bool hashCombination = true);
        string GetMotherboardId();
    }
    public interface ISystemInfoProvider
    {
        System.Collections.Generic.IEnumerable<Orc.SystemInfo.SystemInfoElement> GetSystemInfoElements();
    }
    public interface ISystemInfoService
    {
        System.Collections.Generic.IEnumerable<Orc.SystemInfo.SystemInfoElement> GetSystemInfo();
    }
    public interface IWindowsManagementInformationService
    {
        string GetIdentifier(string wmiClass, string wmiProperty);
        string GetIdentifier(string wmiClass, string wmiProperty, string additionalWmiToCheck, string additionalWmiToCheckValue);
    }
    public static class LongExtensions
    {
        public static string ToReadableSize(this long value, int startUnitIndex = 0) { }
        public static string ToReadableSize(this ulong value, int startUnitIndex = 0) { }
    }
    public class SystemIdentificationService : Orc.SystemInfo.ISystemIdentificationService
    {
        public SystemIdentificationService(Orc.SystemInfo.IWindowsManagementInformationService windowsManagementInformationService) { }
        public virtual string GetCpuId() { }
        public virtual string GetGpuId() { }
        public virtual string GetHardDriveId() { }
        public virtual string GetMacId() { }
        public virtual string GetMachineId(string separator = "-", bool hashCombination = true) { }
        public virtual string GetMotherboardId() { }
        protected static string CalculateMd5Hash(string input) { }
    }
    public class SystemInfoElement
    {
        public SystemInfoElement() { }
        public SystemInfoElement(string name, string value) { }
        public string Name { get; set; }
        public string Value { get; set; }
        public override string ToString() { }
    }
    public class SystemInfoService : Orc.SystemInfo.ISystemInfoService
    {
        public SystemInfoService(Orc.SystemInfo.IDotNetFrameworkService dotNetFrameworkService, Catel.Services.ILanguageService languageService, Orc.SystemInfo.IDbProvidersService dbProviderService, Catel.IoC.IServiceLocator serviceLocator) { }
        public System.Collections.Generic.IEnumerable<Orc.SystemInfo.SystemInfoElement> GetSystemInfo() { }
    }
    public class WindowsManagementInformationService : Orc.SystemInfo.IWindowsManagementInformationService
    {
        public WindowsManagementInformationService() { }
        public string GetIdentifier(string wmiClass, string wmiProperty) { }
        public string GetIdentifier(string wmiClass, string wmiProperty, string additionalWmiToCheck, string additionalWmiToCheckValue) { }
    }
    public static class WindowsManagementObjectExtensions
    {
        public static string GetValue(this Orc.SystemInfo.Wmi.WindowsManagementObject obj, string key, string defaultValue = null) { }
        public static string GetValue<TValue>(this Orc.SystemInfo.Wmi.WindowsManagementObject obj, string key, string defaultValue = null) { }
    }
    public class WmiOperatingSystemSystemInfoProvider : Orc.SystemInfo.ISystemInfoProvider
    {
        public WmiOperatingSystemSystemInfoProvider(Catel.Services.ILanguageService languageService) { }
        public System.Collections.Generic.IEnumerable<Orc.SystemInfo.SystemInfoElement> GetSystemInfoElements() { }
    }
    public class WmiProcessorSystemInfoProvider : Orc.SystemInfo.ISystemInfoProvider
    {
        public WmiProcessorSystemInfoProvider(Catel.Services.ILanguageService languageService) { }
        public System.Collections.Generic.IEnumerable<Orc.SystemInfo.SystemInfoElement> GetSystemInfoElements() { }
    }
}
namespace Orc.SystemInfo.Win32
{
    public static class IWbemClassObjectEnumeratorExtensions { }
    [System.Flags]
    public enum LoadLibraryFlags : uint
    {
        None = 0u,
        DONT_RESOLVE_DLL_REFERENCES = 1u,
        LOAD_IGNORE_CODE_AUTHZ_LEVEL = 16u,
        LOAD_LIBRARY_AS_DATAFILE = 2u,
        LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 64u,
        LOAD_LIBRARY_AS_IMAGE_RESOURCE = 32u,
        LOAD_LIBRARY_SEARCH_APPLICATION_DIR = 512u,
        LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 4096u,
        LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR = 256u,
        LOAD_LIBRARY_SEARCH_SYSTEM32 = 2048u,
        LOAD_LIBRARY_SEARCH_USER_DIRS = 1024u,
        LOAD_WITH_ALTERED_SEARCH_PATH = 8u,
    }
    [System.Flags]
    public enum WbemClassObjectEnumeratorBehaviorOptions
    {
        Bidirectional = 0,
        Prototype = 2,
        ReturnImmediately = 16,
        ForwardOnly = 32,
        DirectRead = 512,
        EnsureLocatable = 256,
        UseAmendedQualifiers = 131072,
    }
    public enum WbemConnectOption
    {
        None = 0,
        UseMaxWait = 128,
    }
    public enum WmiObjectGenus
    {
        Class = 1,
        Instance = 2,
    }
}
namespace Orc.SystemInfo.Wmi
{
    public sealed class WindowsManagementConnection : System.IDisposable
    {
        public WindowsManagementConnection() { }
        public Orc.SystemInfo.Wmi.WindowsManagementQuery CreateQuery(string wql) { }
        public void Dispose() { }
        public Orc.SystemInfo.Wmi.WindowsManagementObjectEnumerator ExecuteQuery(Orc.SystemInfo.Wmi.WindowsManagementQuery query) { }
        public void Open() { }
    }
    public class WindowsManagementObject : System.IDisposable
    {
        public string Class { get; }
        public string[] Derivation { get; }
        public string Dynasty { get; }
        public Orc.SystemInfo.Win32.WmiObjectGenus Genus { get; }
        public object this[string propertyName] { get; }
        public string Namespace { get; }
        public string Path { get; }
        public int PropertyCount { get; }
        public string Relpath { get; }
        public string Server { get; }
        public string SuperClass { get; }
        public void Dispose() { }
        public System.Collections.Generic.IEnumerable<string> GetPropertyNames() { }
        public object GetValue(string propertyName) { }
        public TValue GetValue<TValue>(string propertyName) { }
        public TValue GetValue<TValue>(string propertyName, System.Func<object, TValue> converterFunc) { }
        public override string ToString() { }
    }
    public sealed class WindowsManagementObjectEnumerator : System.Collections.Generic.IEnumerator<Orc.SystemInfo.Wmi.WindowsManagementObject>, System.Collections.IEnumerator, System.IDisposable
    {
        public Orc.SystemInfo.Wmi.WindowsManagementObject Current { get; }
        public void Dispose() { }
        public bool MoveNext() { }
        public void Reset() { }
    }
    public class WindowsManagementQuery : System.Collections.Generic.IEnumerable<Orc.SystemInfo.Wmi.WindowsManagementObject>, System.Collections.IEnumerable
    {
        public WindowsManagementQuery(Orc.SystemInfo.Wmi.WindowsManagementConnection connection, string wql, Orc.SystemInfo.Win32.WbemClassObjectEnumeratorBehaviorOptions enumeratorBehaviorOptions = 16) { }
        public Orc.SystemInfo.Wmi.WindowsManagementConnection Connection { get; }
        public Orc.SystemInfo.Win32.WbemClassObjectEnumeratorBehaviorOptions EnumeratorBehaviorOption { get; }
        public string Wql { get; }
        public System.Collections.Generic.IEnumerator<Orc.SystemInfo.Wmi.WindowsManagementObject> GetEnumerator() { }
    }
}