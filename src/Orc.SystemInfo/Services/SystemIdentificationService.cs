namespace Orc.SystemInfo;

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Catel.Caching;
using Catel.Logging;
using MethodTimer;
using Microsoft.Extensions.Logging;

public class SystemIdentificationService : ISystemIdentificationService
{
    private readonly ILogger<IWindowsManagementInformationService> _logger;
    private readonly IWindowsManagementInformationService _windowsManagementInformationService;

    private readonly ICacheStorage<string, string> _cacheStorage = new CacheStorage<string, string>();

    public SystemIdentificationService(ILogger<IWindowsManagementInformationService> logger, IWindowsManagementInformationService windowsManagementInformationService)
    {
        ArgumentNullException.ThrowIfNull(windowsManagementInformationService);
        _logger = logger;
        _windowsManagementInformationService = windowsManagementInformationService;
    }

    [Time]
    public virtual string GetMachineId(string separator = "-", bool hashCombination = true)
    {
        ArgumentNullException.ThrowIfNull(separator);

        var key = $"machineid_{separator}_{hashCombination}";
        return _cacheStorage.GetFromCacheOrFetch(key, () =>
        {
            _logger.LogDebug("Retrieving machine id");

            var cpuId = string.Empty;
            var motherboardId = string.Empty;
            var hddId = string.Empty;
            var gpuId = string.Empty;

            var tasks = new List<Task>
            {
                Task.Run(() => cpuId = "CPU >> " + GetCpuId()),
                Task.Run(() => motherboardId = "BASE >> " + GetMotherboardId()),
                Task.Run(() => hddId = "HDD >> " + GetHardDriveId()),
                Task.Run(() => gpuId = "GPU >> " + GetGpuId()),
                // Task.Run(() => gpuId = "MAC >> " + _systemIdentificationService.GetMacId())
            };

            Task.WaitAll(tasks.ToArray());

            var values = new List<string>(new[]
            {
                cpuId,
                motherboardId,
                hddId,
                gpuId
            });

            var hashedValues = new List<string>();

            foreach (var value in values)
            {
                var hashedValue = CalculateHash(value);
                hashedValues.Add(hashedValue);

                _logger.LogDebug("* {0} => {1}", value, hashedValue);
            }

            var machineId = string.Join(separator, hashedValues);

            _logger.LogDebug("Hashed machine id '{0}'", machineId);

            if (hashCombination)
            {
                machineId = CalculateHash(machineId);
            }

            return machineId;
        });
    }

    [Time]
    public virtual string GetMacId()
    {
        return _cacheStorage.GetFromCacheOrFetch("MacId", () =>
        {
            var identifier = "Wireless: " + _windowsManagementInformationService.GetIdentifier("Win32_NetworkAdapter", "MACAddress", "AdapterType", "Wireless") +
                             "Wired: " + _windowsManagementInformationService.GetIdentifier("Win32_NetworkAdapter", "MACAddress", "AdapterType", "Ethernet 802.3");

            _logger.LogDebug("Using mac id '{0}'", identifier);

            return identifier;
        });
    }

    [Time]
    public virtual string GetGpuId()
    {
        return _cacheStorage.GetFromCacheOrFetch("GpuId", () =>
        {
            var identifier = _windowsManagementInformationService.GetIdentifier("Win32_VideoController", "DeviceID") +
                             _windowsManagementInformationService.GetIdentifier("Win32_VideoController", "Name");

            _logger.LogDebug("Using gpu id '{0}'", identifier);

            return identifier;
        });
    }

    [Time]
    public virtual string GetHardDriveId()
    {
        return _cacheStorage.GetFromCacheOrFetch("HardDriveId", () =>
        {
            var identifier = _windowsManagementInformationService.GetIdentifier("Win32_DiskDrive", "Model", "InterfaceType", "!USB")
                             + _windowsManagementInformationService.GetIdentifier("Win32_DiskDrive", "Manufacturer", "InterfaceType", "!USB")
                             + _windowsManagementInformationService.GetIdentifier("Win32_DiskDrive", "Signature", "InterfaceType", "!USB")
                             + _windowsManagementInformationService.GetIdentifier("Win32_DiskDrive", "TotalHeads", "InterfaceType", "!USB")
                             + _windowsManagementInformationService.GetIdentifier("Win32_DiskDrive", "DeviceID", "InterfaceType", "!USB")
                             + _windowsManagementInformationService.GetIdentifier("Win32_DiskDrive", "SerialNumber", "InterfaceType", "!USB");

            _logger.LogDebug("Using hdd id '{0}'", identifier);

            return identifier;
        });
    }

    [Time]
    public virtual string GetMotherboardId()
    {
        return _cacheStorage.GetFromCacheOrFetch("MotherboardId", () =>
        {
            // Note: not sure why this returns empty strings on some machines
            var identifier = _windowsManagementInformationService.GetIdentifier("Win32_ComputerSystemProduct", "IdentifyingNumber")
                             + _windowsManagementInformationService.GetIdentifier("Win32_ComputerSystemProduct", "UUID");

            _logger.LogDebug("Using motherboard id '{0}'", identifier);

            return identifier;
        });
    }

    [Time]
    public virtual string GetCpuId()
    {
        return _cacheStorage.GetFromCacheOrFetch("CpuId", () =>
        {
            // Uses first CPU identifier available in order of preference
            var identifier = _windowsManagementInformationService.GetIdentifier("Win32_Processor", "UniqueId");
            if (!string.IsNullOrWhiteSpace(identifier))
            {
                _logger.LogDebug("Using Processor.UniqueId to identify cpu '{0}'", identifier);

                return identifier;
            }

            identifier = _windowsManagementInformationService.GetIdentifier("Win32_Processor", "ProcessorId");
            if (!string.IsNullOrWhiteSpace(identifier))
            {
                _logger.LogDebug("Using Processor.ProcessorId to identify cpu '{0}'", identifier);

                return identifier;
            }

            identifier += _windowsManagementInformationService.GetIdentifier("Win32_Processor", "Name")
                          + _windowsManagementInformationService.GetIdentifier("Win32_Processor", "SerialNumber")
                          + _windowsManagementInformationService.GetIdentifier("Win32_Processor", "Manufacturer")
                          + _windowsManagementInformationService.GetIdentifier("Win32_Processor", "NumberOfCores")
                          + _windowsManagementInformationService.GetIdentifier("Win32_Processor", "NumberOfLogicalProcessors")
                          + _windowsManagementInformationService.GetIdentifier("Win32_Processor", "MaxClockSpeed")
                          + _windowsManagementInformationService.GetIdentifier("Win32_Processor", "Version");

            _logger.LogDebug("Using Processor.Manufacturer + MaxClockSpeed + Version to identify cpu '{0}'", identifier);

            return identifier;
        });
    }

    protected virtual string CalculateHash(string input)
    {
        using var md5 = MD5.Create();
        var inputBytes = Encoding.UTF8.GetBytes(input);
        var hash = md5.ComputeHash(inputBytes);

        var sb = new StringBuilder();

        for (var i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        }

        return sb.ToString();
    }
}
