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
                return datatable.Rows.OfType<DataRow>()
                    .Select(x => x["InvariantName"]?.ToString())
                    .OrderBy(x => x);
            }
        }
    }
}
