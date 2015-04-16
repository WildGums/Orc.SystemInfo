Orc.SystemInfo
==================

This library is used to retrieve the system information from a computer.

Use the GetSystemInfo() method or the ISystemInfoService to get system information.

GetSystemInfo() returns an `IEnumerable<SystemInfoElement>`

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

The following information will be displayed:

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
