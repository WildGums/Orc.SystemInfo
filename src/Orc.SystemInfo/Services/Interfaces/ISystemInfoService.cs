// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISystemInfoService.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
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