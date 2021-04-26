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
    public static class ManagementBaseObjectExtensions
    {
        public static long GetLongValue(this System.Management.ManagementBaseObject obj, string key) { }
        public static string GetValue(this System.Management.ManagementBaseObject obj, string key, string defaultValue = null) { }
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
        public SystemInfoService(Orc.SystemInfo.IDotNetFrameworkService dotNetFrameworkService, Catel.Services.ILanguageService languageService, Orc.SystemInfo.IDbProvidersService dbProviderService) { }
        public System.Collections.Generic.IEnumerable<Orc.SystemInfo.SystemInfoElement> GetSystemInfo() { }
    }
    public class Win32OperatingSystemSystemInfoProvider : Orc.SystemInfo.ISystemInfoProvider
    {
        public Win32OperatingSystemSystemInfoProvider(Catel.Services.ILanguageService languageService) { }
        public System.Collections.Generic.IEnumerable<Orc.SystemInfo.SystemInfoElement> GetSystemInfoElements() { }
        public string ProcessorArchitectureString(ushort processorArchitecture) { }
    }
    public class Win32ProcessorSystemInfoProvider : Orc.SystemInfo.ISystemInfoProvider
    {
        public Win32ProcessorSystemInfoProvider(Catel.Services.ILanguageService languageService) { }
        public System.Collections.Generic.IEnumerable<Orc.SystemInfo.SystemInfoElement> GetSystemInfoElements() { }
    }
    public class WindowsManagementInformationService : Orc.SystemInfo.IWindowsManagementInformationService
    {
        public WindowsManagementInformationService() { }
        public string GetIdentifier(string wmiClass, string wmiProperty) { }
        public string GetIdentifier(string wmiClass, string wmiProperty, string additionalWmiToCheck, string additionalWmiToCheckValue) { }
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
    [System.Flags]
    public enum EnumeratorBehaviorOption
    {
        Bidirectional = 0,
        Prototype = 2,
        ReturnImmediately = 16,
        ForwardOnly = 32,
        DirectRead = 512,
        EnsureLocatable = 256,
        UseAmendedQualifiers = 131072,
    }
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
    public class ProductTypeWindows
    {
        public System.Collections.Generic.Dictionary<int, string> ProductType;
        public ProductTypeWindows() { }
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