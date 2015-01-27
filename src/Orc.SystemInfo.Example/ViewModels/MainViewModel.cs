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

    public class MainViewModel : ViewModelBase
    {
        private readonly ISystemInfoService _systemInfoService;

        public MainViewModel(ISystemInfoService systemInfoService)
        {
            _systemInfoService = systemInfoService;
            ShowSystemInfo = new TaskCommand(OnShowSystemInfoExecute);
        }

        public TaskCommand ShowSystemInfo { get; private set; }

        private async Task OnShowSystemInfoExecute()
        {
            var systemInfo = await _systemInfoService.GetSystemInfo();
            var systemInfoLines = systemInfo.Select(x => string.Format("{0} {1}", x.Name, x.Value));
            SystemInfo = String.Join("\n", systemInfoLines);
        }

        public string SystemInfo { get; set; }
    }
}