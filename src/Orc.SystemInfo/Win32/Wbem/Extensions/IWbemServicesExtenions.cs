namespace Orc.SystemInfo.Win32
{
    using System;

    internal static class IWbemServicesExtenions
    {
        internal static void SetProxy(this IWbemServices wbemServices, ImpersonationLevel impersonationLevel, AuthenticationLevel authenticationLevel)
        {
            HResult hr = WmiNetUtils.CoSetProxyBlanketForIWbemServices.Invoke(wbemServices, impersonationLevel, authenticationLevel);
            if (hr.Failed)
            {
                throw (Exception)hr;
            }
        }

        internal static IWbemClassObjectEnumerator ExecQuery(this IWbemServices wbemServices, string query, WbemClassObjectEnumeratorBehaviorOption enumeratorBehaviorOption, IWbemContext ctx)
        {
            HResult hr = wbemServices.ExecQuery("WQL", query, enumeratorBehaviorOption, ctx, out IWbemClassObjectEnumerator enumerator);

            if (hr.Failed)
            {
                throw (Exception)hr;
            }

            return enumerator;
        }
    }
}
