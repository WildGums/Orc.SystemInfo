# Orc.SystemInfo

Retrieve system information

use method GetSystemInfo() of ISystemInfoService to get system information.

GetSystemInfo() returns `IEnumerable<SystemInfoElement>`

	[Serializable]
    public class SystemInfoElement
    {
        .........

        public string Name { get; set; }
        public string Value { get; set; }

        ......
    }