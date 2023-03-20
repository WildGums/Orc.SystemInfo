namespace Orc.SystemInfo.Example.ViewModels;

using System;
using System.Threading.Tasks;
using Catel.Logging;
using Catel.MVVM;
using Catel.Services;

public class SystemIdentificationViewModel : ViewModelBase
{
    private static readonly ILog Log = LogManager.GetCurrentClassLogger();

    private readonly ISystemIdentificationService _systemIdentificationService;
    private readonly IDispatcherService _dispatcherService;

    public SystemIdentificationViewModel(ISystemIdentificationService systemIdentificationService, IDispatcherService dispatcherService)
    {
        ArgumentNullException.ThrowIfNull(systemIdentificationService);

        _systemIdentificationService = systemIdentificationService;
        _dispatcherService = dispatcherService;
    }

    public bool IsBusy { get; private set; }

    public string? MachineId { get; set; }

    public string? CpuId { get; set; }

    public string? GpuId { get; set; }

    public string? HardDriveId { get; set; }

    public string? MacId { get; set; }

    public string? MotherboardId { get; set; }

    protected override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        IsBusy = true;

        try
        {
            await Task.WhenAll(new[]
            {
                SetValueAsync(() => _systemIdentificationService.GetCpuId(), x => CpuId = x),
                SetValueAsync(() => _systemIdentificationService.GetGpuId(), x => GpuId = x),
                SetValueAsync(() => _systemIdentificationService.GetHardDriveId(), x => HardDriveId = x),
                SetValueAsync(() => _systemIdentificationService.GetMacId(), x => MacId = x),
                SetValueAsync(() => _systemIdentificationService.GetMotherboardId(), x => MotherboardId = x)
            });

            // Note: we calculate the machine id last because we don't want to cause "false timings" in our demo app (the machine id
            // has to wait for all the others to finish so will take much longer then it actually does)
            await Task.Run(() => SetValueAsync(() => _systemIdentificationService.GetMachineId(), x => MachineId = x));
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to get system identification");
        }

        IsBusy = false;
    }

    private async Task SetValueAsync(Func<string> retrievalFunc, Action<string> setter)
    {
        var value = string.Empty;

        await Task.Run(() => value = retrievalFunc());

        _dispatcherService.BeginInvokeIfRequired(() => setter(value));
    }
}
