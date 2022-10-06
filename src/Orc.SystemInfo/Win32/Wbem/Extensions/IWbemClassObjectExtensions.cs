namespace Orc.SystemInfo.Win32
{
    using System;
    using System.Collections.Generic;

    internal static class IWbemClassObjectExtensions
    {
        internal static IEnumerable<string> GetNames(this IWbemClassObject wbemClassObject, WbemConditionFlag flags = WbemConditionFlag.NonSystemOnly)
        {
            var hresult = wbemClassObject.GetNames(null, flags, null, out var names);

            hresult.ThrowIfFailed();

            return names;
        }

        internal static object? Get(this IWbemClassObject wbemClassObjecthis, string propertyName)
        {
            var hresult = wbemClassObjecthis.Get(propertyName, 0, out var value, out var type, out var flavor);

            hresult.ThrowIfFailed();

            return value == DBNull.Value ? null : value;
        }
    }
}
