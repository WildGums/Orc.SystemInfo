// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.SystemInfo.Example.ViewModels
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Catel.MVVM;
    using Catel.Threading;

    public class MainViewModel : ViewModelBase
    {
        private readonly ISystemInfoService _systemInfoService;

        public MainViewModel(ISystemInfoService systemInfoService)
        {
            _systemInfoService = systemInfoService;
            ShowSystemInfo = new TaskCommand(OnShowSystemInfoExecuteAsync);
        }

        public TaskCommand ShowSystemInfo { get; private set; }

        private async Task OnShowSystemInfoExecuteAsync()
        {
            var systemInfo = await TaskHelper.Run(() => _systemInfoService.GetSystemInfo(), true);
            var systemInfoLines = systemInfo.Select(x => string.Format("{0} {1}", x.Name, x.Value));
            SystemInfo = string.Join("\n", systemInfoLines);
        }

        public string SystemInfo { get; set; }
    }
}