namespace Orc.SystemInfo;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Catel;
using Catel.Logging;
using Microsoft.Win32;

public class DotNetFrameworkService : IDotNetFrameworkService
{
    private static readonly ILog Log = LogManager.GetCurrentClassLogger();

    public virtual IEnumerable<string> GetInstalledFrameworks()
    {
        return GetNetFrameworkVersions();
    }

    protected IEnumerable<string> GetNetFrameworkVersions()
    {
        var versions = new List<string>();

        try
        {
            using var ndpKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, string.Empty)
                .OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\");
            if (ndpKey is not null)
            {
                foreach (var versionKeyName in ndpKey.GetSubKeyNames().Where(x => x.StartsWith("v")))
                {
                    using var versionKey = ndpKey.OpenSubKey(versionKeyName);
                    if (versionKey is null)
                    {
                        continue;
                    }

                    foreach (var fullName in BuildFrameworkNamesRecursively(versionKey, versionKeyName, topLevel: true))
                    {
                        if (!string.IsNullOrWhiteSpace(fullName))
                        {
                            versions.Add(fullName);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Log.Warning(ex, "Failed to get .net framework versions");
        }

        return versions;
    }

    protected IEnumerable<string> BuildFrameworkNamesRecursively(RegistryKey registryKey, string name, string topLevelSp = "0", bool topLevel = false)
    {
        ArgumentNullException.ThrowIfNull(registryKey);
        Argument.IsNotNullOrEmpty(() => name);
        Argument.IsNotNullOrEmpty(() => topLevelSp);

        var fullVersion = string.Empty;

        var version = (string?)registryKey.GetValue("Version", string.Empty);
        var sp = registryKey.GetValue("SP", "0")?.ToString();
        var install = registryKey.GetValue("Install", string.Empty)?.ToString();

        if (string.Equals(sp, "0"))
        {
            sp = topLevelSp;
        }

        if (!string.Equals(sp, "0") && string.Equals(install, "1"))
        {
            fullVersion = $"{name} {version} SP{sp}";
        }
        else if (string.Equals(install, "1"))
        {
            fullVersion = $"{name} {version}";
        }

        var topLevelInitialized = !topLevel || !string.IsNullOrEmpty(fullVersion);

        var subNamesCount = 0;
        if (sp is not null)
        {
            foreach (var subKeyName in registryKey.GetSubKeyNames().Where(x => Regex.IsMatch(x, @"^\d{4}$|^Client$|^Full$", RegexOptions.None, TimeSpan.FromSeconds(1))))
            {
                using var subKey = registryKey.OpenSubKey(subKeyName);
                if (subKey is null)
                {
                    continue;
                }

                foreach (var subName in BuildFrameworkNamesRecursively(subKey, $"{name} {subKeyName}", sp, !topLevelInitialized))
                {
                    yield return subName;
                    subNamesCount++;
                }
            }
        }

        if (subNamesCount == 0)
        {
            yield return fullVersion;
        }
    }
}
