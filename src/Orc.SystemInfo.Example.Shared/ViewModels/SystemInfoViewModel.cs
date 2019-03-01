// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemInfoViewModel.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.SystemInfo.Example.ViewModels
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Catel;
    using Catel.Logging;
    using Catel.MVVM;
    using Catel.Threading;

    public class SystemInfoViewModel : ViewModelBase
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly ISystemInfoService _systemInfoService;

        public SystemInfoViewModel(ISystemInfoService systemInfoService)
        {
            Argument.IsNotNull(() => systemInfoService);

            _systemInfoService = systemInfoService;
        }

        public bool IsBusy { get; private set; }

        public string SystemInfo { get; set; }

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            IsBusy = true;

            try
            {
                var systemInfo = await TaskHelper.Run(() => _systemInfoService.GetSystemInfo(), true);
                var systemInfoLines = systemInfo.Select(x => string.Format("{0} {1}", x.Name, x.Value));
                SystemInfo = string.Join("\n", systemInfoLines);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to get system info");
            }

            IsBusy = false;
        }
    }
}
