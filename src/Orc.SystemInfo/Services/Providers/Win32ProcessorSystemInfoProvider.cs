namespace Orc.SystemInfo
{
    using System;
    using System.Collections.Generic;
    using Catel.Logging;
    using Catel.Services;
    using Orc.SystemInfo.Win32;
    using static Orc.SystemInfo.Win32.Kernel32;

    public class Win32ProcessorSystemInfoProvider : ISystemInfoProvider
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly ILanguageService _languageService;

        public Win32ProcessorSystemInfoProvider(ILanguageService languageService)
        {
            _languageService = languageService;
        }
        public IEnumerable<SystemInfoElement> GetSystemInfoElements()
        {
            try
            {
                var items = new List<SystemInfoElement>();

                object o = WmiNetUtils.CoSetProxyBlanketForIWbemServices;

                //using (WmiConnection connection = new WmiConnection())
                //{
                //    foreach (WmiObject partition in connection.CreateQuery("SELECT * FROM Win32_DiskPartition"))
                //    {
                //        Log.Debug($"partition name - {partition["Name"]}");
                //    }
                //}

                return items;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }
    }
}
