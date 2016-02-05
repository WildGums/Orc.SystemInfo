// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemInfoService.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
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
        private readonly IDotNetFrameworkService _dotNetFrameworkService;
        private readonly ILanguageService _languageService;

        public SystemInfoService(IWindowsManagementInformationService windowsManagementInformationService,
            IDotNetFrameworkService dotNetFrameworkService, ILanguageService languageService)
        {
            Argument.IsNotNull(() => windowsManagementInformationService);
            Argument.IsNotNull(() => dotNetFrameworkService);
            Argument.IsNotNull(() => languageService);

            _windowsManagementInformationService = windowsManagementInformationService;
            _dotNetFrameworkService = dotNetFrameworkService;
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
            foreach (var pair in _dotNetFrameworkService.GetInstalledFrameworks())
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
        #endregion
    }
}