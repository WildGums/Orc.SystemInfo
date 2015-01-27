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
    using System.Threading.Tasks;
    using Catel;
    using Microsoft.Win32;

    public class SystemInfoService : ISystemInfoService
    {
        #region ISystemInfoService Members
        public async Task<IEnumerable<SystemInfoElement>> GetSystemInfo()
        {
            return await Task.Factory.StartNew(() =>
            {
                var items = new List<SystemInfoElement>();

                var wmi = new ManagementObjectSearcher("select * from Win32_OperatingSystem")
                    .Get()
                    .Cast<ManagementObject>()
                    .First();

                var cpu = new ManagementObjectSearcher("select * from Win32_Processor")
                    .Get()
                    .Cast<ManagementObject>()
                    .First();

                items.Add(new SystemInfoElement("User name", Environment.UserName));
                items.Add(new SystemInfoElement("User domain name", Environment.UserDomainName));
                items.Add(new SystemInfoElement("Machine name", Environment.MachineName));
                items.Add(new SystemInfoElement("OS version", Environment.OSVersion.ToString()));
                items.Add(new SystemInfoElement("Version", Environment.Version.ToString()));

                items.Add(new SystemInfoElement("OS name", GetObjectValue(wmi, "Caption")));
                items.Add(new SystemInfoElement("MaxProcessRAM", GetObjectValue(wmi, "MaxProcessMemorySize")));
                items.Add(new SystemInfoElement("Architecture", GetObjectValue(wmi, "OSArchitecture")));
                items.Add(new SystemInfoElement("ProcessorId", GetObjectValue(wmi, "ProcessorId")));
                items.Add(new SystemInfoElement("Build", GetObjectValue(wmi, "BuildNumber")));

                items.Add(new SystemInfoElement("CPU name", GetObjectValue(cpu, "Name")));
                items.Add(new SystemInfoElement("Description", GetObjectValue(cpu, "Caption")));
                items.Add(new SystemInfoElement("Address width", GetObjectValue(cpu, "AddressWidth")));
                items.Add(new SystemInfoElement("Data width", GetObjectValue(cpu, "DataWidth")));
                items.Add(new SystemInfoElement("SpeedMHz", GetObjectValue(cpu, "MaxClockSpeed")));
                items.Add(new SystemInfoElement("BusSpeedMHz", GetObjectValue(cpu, "ExtClock")));
                items.Add(new SystemInfoElement("Number of cores", GetObjectValue(cpu, "NumberOfCores")));
                items.Add(new SystemInfoElement("Number of logical processors", GetObjectValue(cpu, "NumberOfLogicalProcessors")));

                items.Add(new SystemInfoElement("Current culture", CultureInfo.CurrentCulture.ToString()));

                items.Add(new SystemInfoElement(".Net Framework versions", string.Empty));
                foreach (var pair in GetNetFrameworkVersions())
                {
                    items.Add(new SystemInfoElement(string.Empty, pair));
                }

                return items;
            });
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