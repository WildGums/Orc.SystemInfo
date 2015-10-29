// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISystemIdentificationService.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
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