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
            var sysInfoLines = _systemInfoService.GetSystemInfo().Select(x => string.Format("{0} {1}", x.Value1, x.Value2));
            SystemInfo = String.Join("\n", sysInfoLines);
        }

        public string SystemInfo { get; set; }
    }
}