// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LongExtensions.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.SystemInfo
{
    using System;

    public static class LongExtensions
    {
        private static readonly string[] SizeSuffixes = {"bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB"};

        public static string SizeConverter(this Int64 value)
        {
            if (value < 0)
            {
                return "-" + SizeConverter(-value);
            }

            var i = 0;

            var finalValue = (decimal) value;

            while (Math.Round(finalValue/1024) >= 1)
            {
                finalValue /= 1024;
                i++;
            }

            return string.Format("{0:N1} {1}", finalValue, SizeSuffixes[i]);
        }
    }
}