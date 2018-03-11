// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManagementBaseObjectExtensions.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.SystemInfo
{
    using System;
    using System.Management;
    using Catel.Logging;

    public static class ManagementBaseObjectExtensions
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public static string GetValue(this ManagementBaseObject obj, string key, string defaultValue = null)
        {
            var finalValue = defaultValue;

            try
            {
                var value = obj[key];
                if (value != null)
                {
                    finalValue = value.ToString();
                }
            }
            catch (ManagementException ex)
            {
                Log.Warning(ex, $"Failed to get value by key '{key}' from ManagementBaseObject");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Failed to get value by key '{key}' from ManagementBaseObject");
            }

            return finalValue;
        }

        public static long GetLongValue(this ManagementBaseObject obj, string key)
        {
            long finalValue = 0;

            try
            {
                var value = obj[key];
                if (value != null)
                {
                    finalValue = Convert.ToInt64(value);
                }
            }
            catch (ManagementException ex)
            {
                Log.Warning(ex, $"Failed to get long value by key '{key}' from ManagementBaseObject");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Failed to get long value by key '{key}' from ManagementBaseObject");
            }

            return finalValue;
        }
    }
}