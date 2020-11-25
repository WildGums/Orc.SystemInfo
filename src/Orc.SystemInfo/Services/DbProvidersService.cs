// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbProvidersService.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


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
            return DbProviderFactories.GetFactoryClasses().Rows.OfType<DataRow>()
                .Select(x => x["InvariantName"]?.ToString())
                .OrderBy(x => x);
        }
    }
}
