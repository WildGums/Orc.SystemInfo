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
