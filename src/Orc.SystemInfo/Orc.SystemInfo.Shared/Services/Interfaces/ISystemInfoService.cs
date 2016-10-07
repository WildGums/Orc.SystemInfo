// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISystemInfoService.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.SystemInfo
{
    using System.Collections.Generic;

    public interface ISystemInfoService
    {
        IEnumerable<SystemInfoElement> GetSystemInfo();
    }
}