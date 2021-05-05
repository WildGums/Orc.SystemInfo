namespace Orc.SystemInfo.Win32
{
    using System;

    internal static class IWbemServicesExtenions
    {
        internal static void SetProxy(this IWbemServices wbemServices, WbemImpersonationLevel impersonationLevel, WbemAuthenticationLevel authenticationLevel)
        {
            var hr = WmiNetUtils.BlessIWbemServices(wbemServices, impersonationLevel, authenticationLevel);
            if (hr.Failed)
            {
                throw (Exception)hr;
            }
        }

        internal static IWbemClassObjectEnumerator ExecQuery(this IWbemServices wbemServices, string query, WbemClassObjectEnumeratorBehaviorOptions enumeratorBehaviorOption, IWbemContext ctx)
        {
            var hr = wbemServices.ExecQuery("WQL", query, enumeratorBehaviorOption, ctx, out var enumerator);

            if (hr.Failed)
            {
                throw (Exception)hr;
            }

            return enumerator;
        }
    }
}
