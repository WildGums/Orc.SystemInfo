namespace Orc.SystemInfo.Win32
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal static class IWbemClassObjectExtensions
    {
        internal static IEnumerable<string> GetNames(this IWbemClassObject wbemClassObject, WbemConditionFlag flags = WbemConditionFlag.WBEM_FLAG_NONSYSTEM_ONLY)
        {
            HResult hr = wbemClassObject.GetNames(null, flags, null, out string[] names);

            if (hr.Failed)
            {
                throw (Exception)hr;
            }

            return names;
        }

        internal static object Get(this IWbemClassObject wbemClassObjecthis, string propertyName)
        {
            HResult hresult = wbemClassObjecthis.Get(propertyName, 0, out object value, out CimType type, out int flavor);

            if (hresult.Failed)
            {
                throw (Exception)hresult;
            }

            return value == DBNull.Value ? null : value;
        }
    }
}
