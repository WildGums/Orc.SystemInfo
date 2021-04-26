namespace Orc.SystemInfo.Win32
{
    using System;
    using System.Threading;

    public static class IWbemClassObjectEnumeratorExtensions
    {
        internal static IWbemClassObject Next(this IWbemClassObjectEnumerator wbemClassObjectEnumerator)
        {
            uint count = 1;
            HResult hresult = wbemClassObjectEnumerator.Next(Timeout.Infinite, count, out IWbemClassObject current, out _);

            if (hresult.Failed)
            {
                throw (Exception)hresult;
            }

            return current;
        }
    }
}
