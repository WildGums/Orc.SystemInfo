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
        public static string GetValue(this ManagementBaseObject obj, string key, string defaultValue = null)
        {
            return GetValue(obj, key, x => x.ToString()) ?? defaultValue;
        }

        public static long GetLongValue(this ManagementBaseObject obj, string key)
        {
            return GetValue(obj, key, Convert.ToInt64);
        }

        private static T GetValue<T>(this ManagementBaseObject obj, string key, Func<object, T> valueRetrievalFunc)
        {
            T finalValue = default(T);

            try
            {
                var value = obj[key];
                if (value is not null)
                {
                    finalValue = valueRetrievalFunc(value);
                }
            }
            catch (ManagementException)
            {
                // Ignore
            }
            catch (Exception)
            {
                // Ignore
            }

            return finalValue;
        }
    }
}
