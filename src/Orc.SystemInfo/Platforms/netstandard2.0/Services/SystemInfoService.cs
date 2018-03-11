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
    using Catel;
    using Catel.Logging;
    using Catel.Services;
    using MethodTimer;

    public class SystemInfoService : ISystemInfoService
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly IDotNetFrameworkService _dotNetFrameworkService;
        private readonly ILanguageService _languageService;
        private readonly IDbProvidersService _dbProviderService;

        public SystemInfoService(IDotNetFrameworkService dotNetFrameworkService, ILanguageService languageService, IDbProvidersService dbProviderService)
        {
            Argument.IsNotNull(() => dotNetFrameworkService);
            Argument.IsNotNull(() => languageService);
            Argument.IsNotNull(() => dbProviderService);

            _dotNetFrameworkService = dotNetFrameworkService;
            _languageService = languageService;
            _dbProviderService = dbProviderService;
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
    }
}