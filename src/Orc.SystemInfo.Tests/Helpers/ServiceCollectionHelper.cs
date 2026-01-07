namespace Orc.SystemInfo.Tests
{
    using Catel;
    using Microsoft.Extensions.DependencyInjection;

    internal static class ServiceCollectionHelper
    {
        public static IServiceCollection CreateServiceCollection()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddLogging();
            serviceCollection.AddCatelCore();
            serviceCollection.AddOrcSystemInfo();

            return serviceCollection;
        }
    }
}
