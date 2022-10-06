namespace Orc.SystemInfo.Win32
{
    internal static class IWbemServicesExtenions
    {
        internal static void SetProxy(this IWbemServices wbemServices, WbemImpersonationLevel impersonationLevel, WbemAuthenticationLevel authenticationLevel)
        {
            var hr = WmiNetUtils.BlessIWbemServices(wbemServices, impersonationLevel, authenticationLevel);

            hr.ThrowIfFailed();
        }

        internal static IWbemClassObjectEnumerator ExecQuery(this IWbemServices wbemServices, string query, WbemClassObjectEnumeratorBehaviorOptions enumeratorBehaviorOption, IWbemContext ctx)
        {
            var hr = wbemServices.ExecQuery("WQL", query, enumeratorBehaviorOption, ctx, out var enumerator);

            hr.ThrowIfFailed();

            return enumerator;
        }
    }
}
