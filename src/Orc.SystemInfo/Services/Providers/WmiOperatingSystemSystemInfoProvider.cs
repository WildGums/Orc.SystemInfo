namespace Orc.SystemInfo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Catel.Logging;
    using Catel.Services;
    using Orc.SystemInfo.Wmi;

    public class WmiOperatingSystemSystemInfoProvider : ISystemInfoProvider
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly ILanguageService _languageService;

        public WmiOperatingSystemSystemInfoProvider(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        public IEnumerable<SystemInfoElement> GetSystemInfoElements()
        {
            var notAvailable = _languageService.GetString("SystemInfo_NotAvailable");
            var items = new List<SystemInfoElement>();

            try
            {
                WindowsManagementObject wmi = null;
                var wql = "SELECT * FROM Win32_OperatingSystem";

                using (var connection = new WindowsManagementConnection())
                {
                    wmi = connection.CreateQuery(wql).FirstOrDefault();
                }

                if (wmi is null)
                {
                    throw Log.ErrorAndCreateException<InvalidOperationException>($"Unexpected result from query: {wql}");
                }

                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_OsName"), wmi.GetValue("Caption", notAvailable)));
                // Note: can be retrieved from SystemInfo.wProcessorAchitecture
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_Architecture"), wmi.GetValue("OSArchitecture", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_Build"), wmi.GetValue("BuildNumber", notAvailable)));
                // Note: can be count from lpMaximumApplicationAddress (Kernel32.SystemInfo);
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_MaxProcossRam"), (wmi.GetValue("MaxProcessMemorySize", Convert.ToInt64)).ToReadableSize(1))); // KB
            }
            catch (Exception ex)
            {
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_OsInfo"), "n/a, please contact support"));
                Log.Warning(ex, "Failed to retrieve OS information");
            }

            return items;
        }
    }
}
