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

        private readonly IDotNetFrameworkService _dotNetFrameworkService;
        private readonly ILanguageService _languageService;
        private readonly IDbProvidersService _dbProviderService;
        private readonly ISystemInfoProvider _win32OperatingSystemSystemInfoProvider;
        private readonly ISystemInfoProvider _win32ProcesorSystemInfoProvider;
        private readonly ISystemInfoProvider _wmiOperatingSystemSystemInfoProvider;
        private readonly ISystemInfoProvider _wmiProcesorSystemInfoProvider;
        public SystemInfoService(IDotNetFrameworkService dotNetFrameworkService, ILanguageService languageService, 
            IDbProvidersService dbProviderService)
        {
            Argument.IsNotNull(() => dotNetFrameworkService);
            Argument.IsNotNull(() => languageService);
            Argument.IsNotNull(() => dbProviderService);

            _dotNetFrameworkService = dotNetFrameworkService;
            _languageService = languageService;
            _dbProviderService = dbProviderService;

            _win32OperatingSystemSystemInfoProvider = new Win32OperatingSystemSystemInfoProvider(languageService);
            _win32ProcesorSystemInfoProvider = new Win32ProcessorSystemInfoProvider(languageService);
            _wmiOperatingSystemSystemInfoProvider = new WmiOperatingSystemSystemInfoProvider(languageService);
            _wmiProcesorSystemInfoProvider = new WmiProcessorSystemInfoProvider(languageService);
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
            
            //________________________________________________________________________
            items.AddRange(_win32OperatingSystemSystemInfoProvider.GetSystemInfoElements());
            items.AddRange(_wmiOperatingSystemSystemInfoProvider.GetSystemInfoElements());
            //________________________________________________________________________

            var memStatus = new Kernel32.MemoryStatusEx();
            if (Kernel32.GlobalMemoryStatusEx(memStatus))
            {
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_TotalMemory"), memStatus.ullTotalPhys.ToReadableSize()));
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_AvailableMemory"), memStatus.ullAvailPhys.ToReadableSize()));
            }

            //________________________________________________________________________

            items.AddRange(_win32ProcesorSystemInfoProvider.GetSystemInfoElements());
            items.AddRange(_wmiProcesorSystemInfoProvider.GetSystemInfoElements());
            //________________________________________________________________________

            items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_SystemUpTime"), GetSystemUpTime()));
            items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_ApplicationUpTime"), (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString()));

            items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_CurrentCulture"), CultureInfo.CurrentCulture.ToString()));

            items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_DotNetFrameworkVersions"), string.Empty));
            foreach (var pair in _dotNetFrameworkService.GetInstalledFrameworks())
            {
                items.Add(new SystemInfoElement(string.Empty, pair));
            }

            items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_InstalledDatabaseProviders"), string.Empty));
            foreach (var dbProviderName in _dbProviderService.GetInstalledDbProviders())
            {
                items.Add(new SystemInfoElement(string.Empty, dbProviderName));
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
