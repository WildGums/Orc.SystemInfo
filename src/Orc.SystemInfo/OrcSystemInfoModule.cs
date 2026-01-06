namespace Orc
{
    using Catel.Services;
    using Catel.ThirdPartyNotices;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Orc.SystemInfo;

    /// <summary>
    /// Core module which allows the registration of default services in the service collection.
    /// </summary>
    public static class OrcSystemInfoModule
    {
        public static IServiceCollection AddOrcSystemInfo(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton<IDotNetFrameworkService, DotNetFrameworkService>();
            serviceCollection.TryAddSingleton<IDbProvidersService, DbProvidersService>();
            serviceCollection.TryAddSingleton<ISystemInfoService, SystemInfoService>();
            serviceCollection.TryAddSingleton<ISystemIdentificationService, SystemIdentificationService>();
            serviceCollection.TryAddSingleton<IWindowsManagementInformationService, WindowsManagementInformationService>();

            serviceCollection.AddSingleton<ISystemInfoProvider, WmiOperatingSystemSystemInfoProvider>();
            serviceCollection.AddSingleton<ISystemInfoProvider, WmiProcessorSystemInfoProvider>();

            serviceCollection.AddSingleton<ILanguageSource>(new LanguageResourceSource("Orc.SystemInfo", "Orc.SystemInfo.Properties", "Resources"));

            serviceCollection.AddSingleton<IThirdPartyNotice>((x) => new LibraryThirdPartyNotice("Orc.SystemInfo", "https://github.com/wildgums/orc.systeminfo"));

            return serviceCollection;
        }
    }
}
