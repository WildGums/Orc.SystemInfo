// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDbProvidersService.cs" company="WildGums">
//   Copyright (c) 2008 - 2018 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.SystemInfo
{
    using System.Collections.Generic;

    public interface IDbProvidersService
    {
        IEnumerable<string> GetInstalledDbProviders();
    }
}