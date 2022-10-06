// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemIdentificationService.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.SystemInfo
{
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using Catel;
    using Catel.Caching;
    using Catel.Logging;
    using MethodTimer;

    public class SystemIdentificationService : ISystemIdentificationService
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly IWindowsManagementInformationService _windowsManagementInformationService;

        private readonly ICacheStorage<string, string> _cacheStorage = new CacheStorage<string, string>();

        public SystemIdentificationService(IWindowsManagementInformationService windowsManagementInformationService)
        {
            Argument.IsNotNull(() => windowsManagementInformationService);

            _windowsManagementInformationService = windowsManagementInformationService;
        }

        [Time]
        public virtual string GetMachineId(string separator = "-", bool hashCombination = true)
        {
            Argument.IsNotNull(() => separator);

            var key = string.Format("machineid_{0}_{1}", separator, hashCombination);
            return _cacheStorage.GetFromCacheOrFetch(key, () =>
            {
                Log.Debug("Retrieving machine id");

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
                    var hashedValue = CalculateMd5Hash(value);
                    hashedValues.Add(hashedValue);

                    Log.Debug("* {0} => {1}", value, hashedValue);
                }

                var machineId = string.Join(separator, hashedValues);

                Log.Debug("Hashed machine id '{0}'", machineId);

                if (hashCombination)
                {
                    machineId = CalculateMd5Hash(machineId);
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

                Log.Debug("Using mac id '{0}'", identifier);

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

                Log.Debug("Using gpu id '{0}'", identifier);

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

                Log.Debug("Using hdd id '{0}'", identifier);

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

                Log.Debug("Using motherboard id '{0}'", identifier);

                return identifier;
            });
        }

        [Time]
        public virtual string GetCpuId()
        {
            return _cacheStorage.GetFromCacheOrFetch("CpuId", () =>
            {
                // Uses first CPU identifier available in order of preference
                var identifier = string.Empty;

                identifier = _windowsManagementInformationService.GetIdentifier("Win32_Processor", "UniqueId");
                if (!string.IsNullOrWhiteSpace(identifier))
                {
                    Log.Debug("Using Processor.UniqueId to identify cpu '{0}'", identifier);

                    return identifier;
                }

                identifier = _windowsManagementInformationService.GetIdentifier("Win32_Processor", "ProcessorId");
                if (!string.IsNullOrWhiteSpace(identifier))
                {
                    Log.Debug("Using Processor.ProcessorId to identify cpu '{0}'", identifier);

                    return identifier;
                }

                identifier += _windowsManagementInformationService.GetIdentifier("Win32_Processor", "Name")
                    + _windowsManagementInformationService.GetIdentifier("Win32_Processor", "SerialNumber")
                    + _windowsManagementInformationService.GetIdentifier("Win32_Processor", "Manufacturer")
                    + _windowsManagementInformationService.GetIdentifier("Win32_Processor", "NumberOfCores")
                    + _windowsManagementInformationService.GetIdentifier("Win32_Processor", "NumberOfLogicalProcessors")
                    + _windowsManagementInformationService.GetIdentifier("Win32_Processor", "MaxClockSpeed")
                    + _windowsManagementInformationService.GetIdentifier("Win32_Processor", "Version");

                Log.Debug("Using Processor.Manufacturer + MaxClockSpeed + Version to identify cpu '{0}'", identifier);

                return identifier;
            });
        }

        protected static string CalculateMd5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
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
    }
}
