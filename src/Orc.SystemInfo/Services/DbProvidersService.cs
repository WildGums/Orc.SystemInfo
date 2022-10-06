namespace Orc.SystemInfo
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;

    public class DbProvidersService : IDbProvidersService
    {
        public IEnumerable<string> GetInstalledDbProviders()
        {
            using (var datatable = DbProviderFactories.GetFactoryClasses())
            {
                var values = datatable.Rows.OfType<DataRow>()
                    .Select(x => x["InvariantName"]?.ToString())
                    .Where(x => x is not null)
                    .Cast<string>()
                    .OrderBy(x => x)
                    .ToList();

                return values;
            }
        }
    }
}
