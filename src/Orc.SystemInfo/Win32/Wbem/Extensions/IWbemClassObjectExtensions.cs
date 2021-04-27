namespace Orc.SystemInfo.Win32
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal static class IWbemClassObjectExtensions
    {
        internal static IEnumerable<string> GetNames(this IWbemClassObject wbemClassObject, WbemConditionFlag flags = WbemConditionFlag.NonSystemOnly)
        {
            var hr = wbemClassObject.GetNames(null, flags, null, out var names);

            if (hr.Failed)
            {
                throw (Exception)hr;
            }

            return names;
        }

        internal static object Get(this IWbemClassObject wbemClassObjecthis, string propertyName)
        {
            var hresult = wbemClassObjecthis.Get(propertyName, 0, out var value, out var type, out var flavor);

            if (hresult.Failed)
            {
                throw (Exception)hresult;
            }

            return value == DBNull.Value ? null : value;
        }
    }
}
