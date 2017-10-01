[assembly: System.Resources.NeutralResourcesLanguageAttribute("en-US")]
[assembly: System.Runtime.InteropServices.ComVisibleAttribute(false)]
[assembly: System.Runtime.Versioning.TargetFrameworkAttribute(".NETFramework,Version=v4.7", FrameworkDisplayName=".NET Framework 4.7")]


public class static ModuleInitializer
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
        [System.Runtime.CompilerServices.IteratorStateMachineAttribute(typeof(Orc.SystemInfo.DotNetFrameworkService.<BuildFrameworkNamesRecursively>d__3))]
        protected System.Collections.Generic.IEnumerable<string> BuildFrameworkNamesRecursively(Microsoft.Win32.RegistryKey registryKey, string name, string topLevelSp = "0", bool topLevel = False) { }
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
        string GetMachineId(string separator = "-", bool hashCombination = True);
        string GetMacId();
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
    public class static LongExtensions
    {
        public static string ToReadableSize(this ulong value) { }
        public static string ToReadableSize(this long value) { }
    }
    public class static ManagementBaseObjectExtensions
    {
        public static long GetLongValue(this System.Management.ManagementBaseObject obj, string key) { }
        public static string GetValue(this System.Management.ManagementBaseObject obj, string key, string defaultValue = null) { }
    }
    public class SystemIdentificationService : Orc.SystemInfo.ISystemIdentificationService
    {
        public SystemIdentificationService(Orc.SystemInfo.IWindowsManagementInformationService windowsManagementInformationService) { }
        protected static string CalculateMd5Hash(string input) { }
        [MethodTimer.TimeAttribute()]
        public virtual string GetCpuId() { }
        [MethodTimer.TimeAttribute()]
        public virtual string GetGpuId() { }
        [MethodTimer.TimeAttribute()]
        public virtual string GetHardDriveId() { }
        [MethodTimer.TimeAttribute()]
        public virtual string GetMachineId(string separator = "-", bool hashCombination = True) { }
        [MethodTimer.TimeAttribute()]
        public virtual string GetMacId() { }
        [MethodTimer.TimeAttribute()]
        public virtual string GetMotherboardId() { }
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
        public SystemInfoService(Orc.SystemInfo.IWindowsManagementInformationService windowsManagementInformationService, Orc.SystemInfo.IDotNetFrameworkService dotNetFrameworkService, Catel.Services.ILanguageService languageService, Orc.SystemInfo.IDbProvidersService dbProviderService) { }
        [MethodTimer.TimeAttribute()]
        public System.Collections.Generic.IEnumerable<Orc.SystemInfo.SystemInfoElement> GetSystemInfo() { }
    }
    public class WindowsManagementInformationService : Orc.SystemInfo.IWindowsManagementInformationService
    {
        public WindowsManagementInformationService() { }
        public string GetIdentifier(string wmiClass, string wmiProperty) { }
        public string GetIdentifier(string wmiClass, string wmiProperty, string additionalWmiToCheck, string additionalWmiToCheckValue) { }
    }
}