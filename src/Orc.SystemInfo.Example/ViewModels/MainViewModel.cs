namespace Orc.SystemInfo.Example.ViewModels;

using System;
using Catel.MVVM;

public class MainViewModel : ViewModelBase
{
    public MainViewModel(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
        Title = "Orc.SystemInfo example";
    }
}
