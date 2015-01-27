// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemInfoService.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.SystemInfo
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Management;
    using System.Text.RegularExpressions;
    using Catel;
    using Microsoft.Win32;

    internal class SystemInfoService : ISystemInfoService
    {
        #region ISystemInfoService Members
        public IEnumerable<SystemInfoElement> GetSystemInfo()
        {
            var wmi = new ManagementObjectSearcher("select * from Win32_OperatingSystem")
                .Get()
                .Cast<ManagementObject>()
                .First();

            var cpu = new ManagementObjectSearcher("select * from Win32_Processor")
                .Get()
                .Cast<ManagementObject>()
                .First();

            yield return new SystemInfoElement("User name", Environment.UserName);
            yield return new SystemInfoElement("User domain name", Environment.UserDomainName);
            yield return new SystemInfoElement("Machine name", Environment.MachineName);
            yield return new SystemInfoElement("OS version", Environment.OSVersion.ToString());
            yield return new SystemInfoElement("Version", Environment.Version.ToString());

            yield return new SystemInfoElement("OS name", GetObjectValue(wmi, "Caption"));
            yield return new SystemInfoElement("MaxProcessRAM", GetObjectValue(wmi, "MaxProcessMemorySize"));
            yield return new SystemInfoElement("Architecture", GetObjectValue(wmi, "OSArchitecture"));
            yield return new SystemInfoElement("ProcessorId", GetObjectValue(wmi, "ProcessorId"));
            yield return new SystemInfoElement("Build", GetObjectValue(wmi, "BuildNumber"));

            yield return new SystemInfoElement("CPU name", GetObjectValue(cpu, "Name"));
            yield return new SystemInfoElement("Description", GetObjectValue(cpu, "Caption"));
            yield return new SystemInfoElement("Address width", GetObjectValue(cpu, "AddressWidth"));
            yield return new SystemInfoElement("Data width", GetObjectValue(cpu, "DataWidth"));
            yield return new SystemInfoElement("SpeedMHz", GetObjectValue(cpu, "MaxClockSpeed"));
            yield return new SystemInfoElement("BusSpeedMHz", GetObjectValue(cpu, "ExtClock"));
            yield return new SystemInfoElement("Number of cores", GetObjectValue(cpu, "NumberOfCores"));
            yield return new SystemInfoElement("Number of logical processors", GetObjectValue(cpu, "NumberOfLogicalProcessors"));

            yield return new SystemInfoElement("Current culture", CultureInfo.CurrentCulture.ToString());

            yield return new SystemInfoElement(".Net Framework versions", string.Empty);
            foreach (var pair in GetNetFrameworkVersions())
            {
                yield return new SystemInfoElement(string.Empty, pair);
            }
        }
        #endregion

        #region Methods
        private string GetObjectValue(ManagementObject obj, string key)
        {
            var finalValue = "n/a";

            try
            {
                var value = obj[key];
                if (value != null)
                {
                    finalValue = value.ToString();
                }
            }
            catch (ManagementException)
            {
            }
            catch (Exception)
            {
            }

            return finalValue;
        }

        private static IEnumerable<string> GetNetFrameworkVersions()
        {
            using (var ndpKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, string.Empty)
                .OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\"))
            {
                foreach (var versionKeyName in ndpKey.GetSubKeyNames().Where(x => x.StartsWith("v")))
                {
                    using (var versionKey = ndpKey.OpenSubKey(versionKeyName))
                    {
                        foreach (var fullName in BuildFrameworkNamesRecursively(versionKey, versionKeyName, topLevel:true))
                        {
                            if (!string.IsNullOrWhiteSpace(fullName))
                            {
                                yield return fullName;
                            }
                        }
                    }
                }
            }
        }

        private static IEnumerable<string> BuildFrameworkNamesRecursively(RegistryKey registryKey, string name, string topLevelSp = "0", bool topLevel = false)
        {
            Argument.IsNotNull(() => registryKey);
            Argument.IsNotNullOrEmpty(() => name);
            Argument.IsNotNullOrEmpty(() => topLevelSp);

            if (registryKey == null)
            {
                yield break;
            }

            var fullVersion = string.Empty;

            var version = (string) registryKey.GetValue("Version", string.Empty);
            var sp = registryKey.GetValue("SP", "0").ToString();
            var install = registryKey.GetValue("Install", string.Empty).ToString();

            if (string.Equals(sp, "0"))
            {
                sp = topLevelSp;
            }

            if (!string.Equals(sp, "0") && string.Equals(install, "1"))
            {
                fullVersion = string.Format("{0} {1} SP{2}", name, version, sp);
            }
            else if (string.Equals(install, "1"))
            {
                fullVersion = string.Format("{0} {1}", name, version);
            }

            var topLevelInitialized = !topLevel || !string.IsNullOrEmpty(fullVersion);

            var subnamesCount = 0;
            foreach (var subKeyName in registryKey.GetSubKeyNames().Where(x => Regex.IsMatch(x, @"^\d{4}$|^Client$|^Full$")))
            {
                using (var subKey = registryKey.OpenSubKey(subKeyName))
                {
                    foreach (var subName in BuildFrameworkNamesRecursively(subKey, string.Format("{0} {1}", name, subKeyName), sp, !topLevelInitialized))
                    {
                        yield return subName;
                        subnamesCount++;
                    }
                }
            }

            if (subnamesCount == 0)
            {
                yield return fullVersion;
            }
        }
        #endregion
    }
}