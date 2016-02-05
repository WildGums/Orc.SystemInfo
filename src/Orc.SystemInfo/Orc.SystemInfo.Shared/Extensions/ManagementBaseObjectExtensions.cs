// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManagementObjectExtensions.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.SystemInfo
{
    using System;
    using System.Management;

    public static class ManagementBaseObjectExtensions
    {
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
            catch (ManagementException)
            {
            }
            catch (Exception)
            {
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
            catch (ManagementException)
            {
            }
            catch (Exception)
            {
            }

            return finalValue;
        }
    }
}