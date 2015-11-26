// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemInfoService.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.SystemInfo
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Management;
    using System.Text.RegularExpressions;
    using Catel;
    using Catel.Logging;
    using Catel.Services;
    using MethodTimer;
    using Microsoft.Win32;
    using Win32;

    public class SystemInfoService : ISystemInfoService
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly IWindowsManagementInformationService _windowsManagementInformationService;
        private readonly ILanguageService _languageService;

        public SystemInfoService(IWindowsManagementInformationService windowsManagementInformationService,
            ILanguageService languageService)
        {
            Argument.IsNotNull(() => windowsManagementInformationService);
            Argument.IsNotNull(() => languageService);

            _windowsManagementInformationService = windowsManagementInformationService;
            _languageService = languageService;
        }

        #region ISystemInfoService Members
        [Time]
        public IEnumerable<SystemInfoElement> GetSystemInfo()
        {
            Log.Debug("Retrieving system info");

            var notAvailable = _languageService.GetString("SystemInfo_NotAvailable");
               
            var items = new List<SystemInfoElement>();

            items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_UserName"), Environment.UserName));
            items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_UserDomainName"), Environment.UserDomainName));
            items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_MachineName"), Environment.MachineName));
            items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_OsVersion"), Environment.OSVersion.ToString()));
            items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_Version"), Environment.Version.ToString()));

            try
            {
                var wmi = new ManagementObjectSearcher("select * from Win32_OperatingSystem")
                    .Get()
                    .Cast<ManagementObject>()
                    .First();

                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_OsName"), wmi.GetValue("Caption", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_Architecture"), wmi.GetValue("OSArchitecture", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_ProcessorId"), wmi.GetValue("ProcessorId", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_Build"), wmi.GetValue("BuildNumber", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_MaxProcossRam"), (wmi.GetLongValue("MaxProcessMemorySize")).ToReadableSize()));
            }
            catch (Exception ex)
            {
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_OsInfo"), "n/a, please contact support"));
                Log.Warning(ex, "Failed to retrieve OS information");
            }

            var memStatus = new Kernel32.MEMORYSTATUSEX();
            if (Kernel32.GlobalMemoryStatusEx(memStatus))
            {
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_TotalMemory"), memStatus.ullTotalPhys.ToReadableSize()));
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_AvailableMemory"), memStatus.ullAvailPhys.ToReadableSize()));
            }

            try
            {
                var cpu = new ManagementObjectSearcher("select * from Win32_Processor")
                    .Get()
                    .Cast<ManagementObject>()
                    .First();

                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_CpuName"), cpu.GetValue("Name", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_Description"), cpu.GetValue("Caption", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_AddressWidth"), cpu.GetValue("AddressWidth", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_DataWidth"), cpu.GetValue("DataWidth", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_ClockSpeedMHz"), cpu.GetValue("MaxClockSpeed", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_BusSpeedMHz"), cpu.GetValue("ExtClock", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_NumberOfCores"), cpu.GetValue("NumberOfCores", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_NumberOfLogicalProcessors"), cpu.GetValue("NumberOfLogicalProcessors", notAvailable)));
            }
            catch (Exception ex)
            {
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_CpuInfo"), "n/a, please contact support"));
                Log.Warning(ex, "Failed to retrieve CPU information");
            }

            items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_SystemUpTime"), GetSystemUpTime()));
            items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_ApplicationUpTime"), (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString()));

            items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_CurrentCulture"), CultureInfo.CurrentCulture.ToString()));

            items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_DotNetFrameworkVersions"), string.Empty));
            foreach (var pair in GetNetFrameworkVersions())
            {
                items.Add(new SystemInfoElement(string.Empty, pair));
            }

            Log.Debug("Retrieved system info");

            return items;
        }
        #endregion

        #region Methods
        private static string GetSystemUpTime()
        {
            try
            {
                var upTime = new PerformanceCounter("System", "System Up Time");
                upTime.NextValue();
                return TimeSpan.FromSeconds(upTime.NextValue()).ToString();
            }
            catch (Exception)
            {
                return "n/a";
            }
        }

        private static IEnumerable<string> GetNetFrameworkVersions()
        {
            var versions = new List<string>();

            try
            {
                using (var ndpKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, string.Empty)
                    .OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\"))
                {
                    foreach (var versionKeyName in ndpKey.GetSubKeyNames().Where(x => x.StartsWith("v")))
                    {
                        using (var versionKey = ndpKey.OpenSubKey(versionKeyName))
                        {
                            foreach (var fullName in BuildFrameworkNamesRecursively(versionKey, versionKeyName, topLevel: true))
                            {
                                if (!string.IsNullOrWhiteSpace(fullName))
                                {
                                    versions.Add(fullName);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Warning(ex, "Failed to get .net framework versions");
            }

            return versions;
        }

        private static IEnumerable<string> BuildFrameworkNamesRecursively(RegistryKey registryKey, string name, string topLevelSp = "0", bool topLevel = false)
        {
            Argument.IsNotNull(() => registryKey);
            Argument.IsNotNullOrEmpty(() => name);
            Argument.IsNotNullOrEmpty(() => topLevelSp);

            if (registryKey == null)
            {
                yield break;
            }

            var fullVersion = string.Empty;

            var version = (string)registryKey.GetValue("Version", string.Empty);
            var sp = registryKey.GetValue("SP", "0").ToString();
            var install = registryKey.GetValue("Install", string.Empty).ToString();

            if (string.Equals(sp, "0"))
            {
                sp = topLevelSp;
            }

            if (!string.Equals(sp, "0") && string.Equals(install, "1"))
            {
                fullVersion = string.Format("{0} {1} SP{2}", name, version, sp);
            }
            else if (string.Equals(install, "1"))
            {
                fullVersion = string.Format("{0} {1}", name, version);
            }

            var topLevelInitialized = !topLevel || !string.IsNullOrEmpty(fullVersion);

            var subnamesCount = 0;
            foreach (var subKeyName in registryKey.GetSubKeyNames().Where(x => Regex.IsMatch(x, @"^\d{4}$|^Client$|^Full$")))
            {
                using (var subKey = registryKey.OpenSubKey(subKeyName))
                {
                    foreach (var subName in BuildFrameworkNamesRecursively(subKey, string.Format("{0} {1}", name, subKeyName), sp, !topLevelInitialized))
                    {
                        yield return subName;
                        subnamesCount++;
                    }
                }
            }

            if (subnamesCount == 0)
            {
                yield return fullVersion;
            }
        }
        #endregion
    }
}