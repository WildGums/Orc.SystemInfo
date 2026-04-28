namespace Orc.SystemInfo.Example.ViewModels;

using System;
using System.Linq;
using System.Threading.Tasks;
using Catel.Logging;
using Catel.MVVM;
using Microsoft.Extensions.Logging;

public class SystemInfoViewModel : ViewModelBase
{
    private static readonly ILogger Logger = LogManager.GetLogger(typeof(SystemInfoViewModel));

    private readonly ISystemInfoService _systemInfoService;

    public SystemInfoViewModel(ISystemInfoService systemInfoService, IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
        _systemInfoService = systemInfoService;
    }

    public bool IsBusy { get; private set; }

    public string? SystemInfo { get; set; }

    protected override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        IsBusy = true;

        try
        {
            var systemInfo = await Task.Run(() => _systemInfoService.GetSystemInfo());
            var systemInfoLines = systemInfo.Select(x => $"{x.Name} {x.Value}");
            SystemInfo = string.Join("\n", systemInfoLines);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Failed to get system info");
        }

        IsBusy = false;
    }
}
