// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbProvidersService.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.SystemInfo
{
    using System;
    using System.Collections.Generic;
    using Catel.Logging;

    public class DbProvidersService : IDbProvidersService
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public IEnumerable<string> GetInstalledDbProviders()
        {
            throw Log.ErrorAndCreateException<PlatformNotSupportedException>(string.Empty);
        }
    }
}
