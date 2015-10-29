// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IdentificationViewModel.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.SystemInfo.Example.ViewModels
{
    using System;
    using System.Threading.Tasks;
    using Catel;
    using Catel.MVVM;
    using Catel.Services;
    using Catel.Threading;

    public class SystemIdentificationViewModel : ViewModelBase
    {
        private readonly ISystemIdentificationService _systemIdentificationService;
        private readonly IDispatcherService _dispatcherService;

        public SystemIdentificationViewModel(ISystemIdentificationService systemIdentificationService, IDispatcherService dispatcherService)
        {
            Argument.IsNotNull(() => systemIdentificationService);

            _systemIdentificationService = systemIdentificationService;
            _dispatcherService = dispatcherService;
        }

        public bool IsBusy { get; private set; }

        public string MachineId { get; set; }

        public string CpuId { get; set; }

        public string GpuId { get; set; }

        public string HardDriveId { get; set; }

        public string MacId { get; set; }

        public string MotherboardId { get; set; }

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            IsBusy = true;

            await TaskShim.WhenAll(new []
            {
                SetValueAsync(() => _systemIdentificationService.GetMachineId(), x => MachineId = x),
                SetValueAsync(() => _systemIdentificationService.GetCpuId(), x => CpuId = x),
                SetValueAsync(() => _systemIdentificationService.GetGpuId(), x => GpuId = x),
                SetValueAsync(() => _systemIdentificationService.GetHardDriveId(), x => HardDriveId = x),
                SetValueAsync(() => _systemIdentificationService.GetMacId(), x => MacId = x),
                SetValueAsync(() => _systemIdentificationService.GetMotherboardId(), x => MotherboardId = x)
            });

            IsBusy = false;
        }

        private async Task SetValueAsync(Func<string> retrievalFunc, Action<string> setter)
        {
            var value = string.Empty;

            await TaskHelper.Run(() => value = retrievalFunc(), true);

            _dispatcherService.BeginInvokeIfRequired(() => setter(value));
        }
    }
}