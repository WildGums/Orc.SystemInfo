Orc.SystemInfo
==================

[![Join the chat at https://gitter.im/WildGums/Orc.SystemInfo](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/WildGums/Orc.SystemInfo?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

![License](https://img.shields.io/github/license/wildgums/orc.systeminfo.svg)
![NuGet downloads](https://img.shields.io/nuget/dt/orc.systeminfo.svg)
![Version](https://img.shields.io/nuget/v/orc.systeminfo.svg)
![Pre-release version](https://img.shields.io/nuget/vpre/orc.systeminfo.svg)

This library is used to retrieve the system information details from a computer.

Use the `GetSystemInfo()` method or the `ISystemInfoService` to get the system information details.

`GetSystemInfo()` returns an `IEnumerable<SystemInfoElement>`

```c#
[Serializable]
public class SystemInfoElement
{
    ...
    public string Name { get; set; }
    public string Value { get; set; }
    ...
}
```

The following information will be retreived:

- User name
- User domain
- Machine name
- OS version
- OS name Microsoft
- MaxProcessRAM
- Architecture
- ProcessorId 
- Build 
- CPU name 
- Description
- Address width 
- Data width 
- SpeedMHz
- BusSpeedMHz
- Number of cores
- Number of logical processors
- System up time
- Application up time
- Total memory
- Available memory
- Current culture
- .Net Framework versions  
