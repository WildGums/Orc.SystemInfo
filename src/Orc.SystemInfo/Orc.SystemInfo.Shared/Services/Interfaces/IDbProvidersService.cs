// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDbProvidersService.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


using System.Collections.Generic;

namespace Orc.SystemInfo
{
    public interface IDbProvidersService
    {
        IEnumerable<string> GetInstalledDbProviders();
    }
}
