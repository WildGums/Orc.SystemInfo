using Catel.IoC;
using Catel.Services;
using Catel.Services.Models;
using Orc.SystemInfo;

/// <summary>
/// Used by the ModuleInit. All code inside the Initialize method is ran as soon as the assembly is loaded.
/// </summary>
public static class ModuleInitializer
{
    /// <summary>
    /// Initializes the module.
    /// </summary>
    public static void Initialize()
    {
        var serviceLocator = ServiceLocator.Default;

        serviceLocator.RegisterType<IWindowsManagementInformationService, WindowsManagementInformationService>();
        serviceLocator.RegisterType<ISystemInfoService, SystemInfoService>();
        serviceLocator.RegisterType<ISystemIdentificationService, SystemIdentificationService>();

        var languageService = serviceLocator.ResolveType<ILanguageService>();
        languageService.RegisterLanguageSource(new LanguageResourceSource("Orc.SystemInfo", "Orc.SystemInfo.Properties", "Resources"));
    }
}