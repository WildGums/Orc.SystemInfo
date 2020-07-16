[assembly: System.Resources.NeutralResourcesLanguage("en-US")]
[assembly: System.Runtime.InteropServices.ComVisible(false)]
[assembly: System.Runtime.Versioning.TargetFramework(".NETCoreApp,Version=v3.1", FrameworkDisplayName="")]
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
        public static string ToReadableSize(this long value) { }
        public static string ToReadableSize(this ulong value) { }
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
    public class WindowsManagementInformationService : Orc.SystemInfo.IWindowsManagementInformationService
    {
        public WindowsManagementInformationService() { }
        public string GetIdentifier(string wmiClass, string wmiProperty) { }
        public string GetIdentifier(string wmiClass, string wmiProperty, string additionalWmiToCheck, string additionalWmiToCheckValue) { }
    }
}