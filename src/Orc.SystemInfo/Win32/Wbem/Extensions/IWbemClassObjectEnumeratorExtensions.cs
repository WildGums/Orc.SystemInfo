namespace Orc.SystemInfo.Win32;

using System;
using System.Threading;

public static class IWbemClassObjectEnumeratorExtensions
{
    internal static IWbemClassObject Next(this IWbemClassObjectEnumerator wbemClassObjectEnumerator)
    {
        const uint count = 1;
        var hresult = wbemClassObjectEnumerator.Next(Timeout.Infinite, count, out var current, out _);

        hresult.ThrowIfFailed();

        return current;
    }
}
