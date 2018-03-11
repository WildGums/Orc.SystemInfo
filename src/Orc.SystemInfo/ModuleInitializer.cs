using Catel.IoC;
using Catel.Services;
using Orc.SystemInfo;

/// <summary>
/// Used by the ModuleInit. All code inside the Initialize method is ran as soon as the assembly is loaded.
/// </summary>
public static partial class ModuleInitializer
{
    /// <summary>
    /// Initializes the module.
    /// </summary>
    public static void Initialize()
    {
        var serviceLocator = ServiceLocator.Default;

        serviceLocator.RegisterType<IDotNetFrameworkService, DotNetFrameworkService>();
        serviceLocator.RegisterType<IDbProvidersService, DbProvidersService>();
        serviceLocator.RegisterType<ISystemInfoService, SystemInfoService>();
        serviceLocator.RegisterType<ISystemIdentificationService, SystemIdentificationService>();

        var languageService = serviceLocator.ResolveType<ILanguageService>();
        languageService.RegisterLanguageSource(new LanguageResourceSource("Orc.SystemInfo", "Orc.SystemInfo.Properties", "Resources"));
        
        InitializeNet(serviceLocator);
        InitializeNetStandard_2_0(serviceLocator);
        InitializeUap_10_0(serviceLocator);
    }
    
    static partial void InitializeNet(IServiceLocator serviceLocator);
    static partial void InitializeNetStandard_2_0(IServiceLocator serviceLocator);
    static partial void InitializeUap_10_0(IServiceLocator serviceLocator);
}