namespace Orc.SystemInfo.Win32;

internal static class IWbemLocatorExtensions
{
    /// <summary>
    /// Connect Server with default call parameters
    /// </summary>
    /// <param name="wbemLocator"></param>
    /// <param name="resource"></param>
    /// <param name="ctx"></param>
    /// <returns></returns>
    internal static IWbemServices? ConnectServer(this IWbemLocator wbemLocator, string resource, IWbemContext? ctx)
    {
        var hr = wbemLocator.ConnectServer(resource, null, null, null, WbemConnectOption.None, null, ctx, out var services);

        hr.ThrowIfFailed();

        return services;
    }
}
