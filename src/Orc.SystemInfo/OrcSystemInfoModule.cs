namespace Orc.SystemInfo
{
    using Catel.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    /// <summary>
    /// Core module which allows the registration of default services in the service collection.
    /// </summary>
    public static class OrcSystemInfoModule
    {
        public static IServiceCollection AddOrcSystemInfoServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton<IDotNetFrameworkService, DotNetFrameworkService>();
            serviceCollection.TryAddSingleton<IDbProvidersService, DbProvidersService>();
            serviceCollection.TryAddSingleton<ISystemInfoService, SystemInfoService>();
            serviceCollection.TryAddSingleton<ISystemIdentificationService, SystemIdentificationService>();
            serviceCollection.TryAddSingleton<IWindowsManagementInformationService, WindowsManagementInformationService>();

            serviceCollection.AddSingleton<ISystemInfoProvider, WmiOperatingSystemSystemInfoProvider>();
            serviceCollection.AddSingleton<ISystemInfoProvider, WmiProcessorSystemInfoProvider>();

            serviceCollection.AddSingleton<ILanguageSource>(new LanguageResourceSource("Orc.SystemInfo", "Orc.SystemInfo.Properties", "Resources"));

            return serviceCollection;
        }
    }
}
