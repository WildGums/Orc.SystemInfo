namespace Orc.SystemInfo.Win32
{
    using System;
    using Catel.Logging;

    internal static class IWbemLocatorExtensions
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Connect Server with default call parameters
        /// </summary>
        /// <param name="wbemLocator"></param>
        /// <param name="resource"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        internal static IWbemServices ConnectServer(this IWbemLocator wbemLocator, string resource, IWbemContext ctx)
        {
            var hr = wbemLocator.ConnectServer(resource, null, null, null, WbemConnectOption.None, null, ctx, out var services);

            if (hr.Failed)
            {
                var exception = (Exception)hr;
                Log.Error(exception);
                throw exception;
            }

            return services;
        }
    }
}
