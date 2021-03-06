﻿namespace Orc.SystemInfo.Win32
{
    using System;
    using System.Threading;

    public static class IWbemClassObjectEnumeratorExtensions
    {
        internal static IWbemClassObject Next(this IWbemClassObjectEnumerator wbemClassObjectEnumerator)
        {
            uint count = 1;
            var hresult = wbemClassObjectEnumerator.Next(Timeout.Infinite, count, out var current, out _);

            if (hresult.Failed)
            {
                throw (Exception)hresult;
            }

            return current;
        }
    }
}
