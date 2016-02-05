// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISystemIdentificationService.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.SystemInfo
{
    public interface ISystemIdentificationService
    {
        string GetMachineId(string separator = "-", bool hashCombination = true);
        string GetMacId();
        string GetGpuId();
        string GetHardDriveId();
        string GetMotherboardId();
        string GetCpuId();
    }
}