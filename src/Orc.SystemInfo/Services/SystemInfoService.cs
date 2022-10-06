namespace Orc.SystemInfo
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using Catel.IoC;
    using Catel.Logging;
    using Catel.Services;
    using MethodTimer;
    using Win32;

    public class SystemInfoService : ISystemInfoService
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly IDotNetFrameworkService _dotNetFrameworkService;
        private readonly ILanguageService _languageService;
        private readonly IDbProvidersService _dbProviderService;
        private readonly ISystemInfoProvider _wmiOperatingSystemSystemInfoProvider;
        private readonly ISystemInfoProvider _wmiProcesorSystemInfoProvider;

        public SystemInfoService(IDotNetFrameworkService dotNetFrameworkService, ILanguageService languageService,
            IDbProvidersService dbProviderService, IServiceLocator serviceLocator)
        {
            ArgumentNullException.ThrowIfNull(dotNetFrameworkService);
            ArgumentNullException.ThrowIfNull(languageService);
            ArgumentNullException.ThrowIfNull(dbProviderService);
            ArgumentNullException.ThrowIfNull(serviceLocator);

            _dotNetFrameworkService = dotNetFrameworkService;
            _languageService = languageService;
            _dbProviderService = dbProviderService;

            _wmiOperatingSystemSystemInfoProvider = serviceLocator.ResolveRequiredType<ISystemInfoProvider>(Constants.CimNamespaces.OperatingSystem);
            _wmiProcesorSystemInfoProvider = serviceLocator.ResolveRequiredType<ISystemInfoProvider>(Constants.CimNamespaces.Processor);
        }

        [Time]
        public IEnumerable<SystemInfoElement> GetSystemInfo()
        {
            Log.Debug("Retrieving system info");

            var items = new List<SystemInfoElement>
            {
                new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_UserName"), Environment.UserName),
                new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_UserDomainName"), Environment.UserDomainName),
                new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_MachineName"), Environment.MachineName),
                new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_OsVersion"), Environment.OSVersion.ToString()),
                new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_Version"), Environment.Version.ToString())
            };

            items.AddRange(_wmiOperatingSystemSystemInfoProvider.GetSystemInfoElements());

            var memStatus = new Kernel32.MemoryStatusEx();
            if (Kernel32.GlobalMemoryStatusEx(memStatus))
            {
                items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_TotalMemory"), memStatus.ullTotalPhys.ToReadableSize()));
                items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_AvailableMemory"), memStatus.ullAvailPhys.ToReadableSize()));
            }

            items.AddRange(_wmiProcesorSystemInfoProvider.GetSystemInfoElements());

            items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_SystemUpTime"), GetSystemUpTime()));
            items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_ApplicationUpTime"), (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString()));

            items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_CurrentCulture"), CultureInfo.CurrentCulture.ToString()));

            items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_DotNetFrameworkVersions"), string.Empty));

            foreach (var pair in _dotNetFrameworkService.GetInstalledFrameworks())
            {
                items.Add(new SystemInfoElement(string.Empty, pair));
            }

            items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_InstalledDatabaseProviders"), string.Empty));

            foreach (var dbProviderName in _dbProviderService.GetInstalledDbProviders())
            {
                items.Add(new SystemInfoElement(string.Empty, dbProviderName));
            }

            Log.Debug("Retrieved system info");

            return items;
        }

        private static string GetSystemUpTime()
        {
            try
            {
                using (var upTime = new PerformanceCounter("System", "System Up Time"))
                {
                    upTime.NextValue();
                    return TimeSpan.FromSeconds(upTime.NextValue()).ToString();
                }
            }
            catch (Exception)
            {
                return "n/a";
            }
        }
    }
}
