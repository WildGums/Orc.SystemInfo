﻿namespace Orc.SystemInfo;

using System;

public static class LongExtensions
{
    private static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

    public static string ToReadableSize(this ulong value, int startUnitIndex = 0)
    {
        return ToReadableSize((long)value, startUnitIndex);
    }

    public static string ToReadableSize(this long value, int startUnitIndex = 0)
    {
        if (value < 0)
        {
            return "-" + ToReadableSize(-value);
        }

        var i = startUnitIndex;

        var finalValue = (decimal)value;

        while (Math.Round(finalValue / 1024) >= 1)
        {
            finalValue /= 1024;
            i++;
        }

        return $"{finalValue:N2} {SizeSuffixes[i]}";
    }
}
