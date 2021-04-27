namespace Orc.SystemInfo.Wmi
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using Catel;
    using Catel.Logging;
    using Orc.SystemInfo.Win32;

    /// <summary>
    /// Represent object bound to wbem object
    /// </summary>
    public class WindowsManagementObject : IDisposable
    {
        private const string ClassPropertyName = "__class";
        private const string DerivationPropertyName = "__derivation";
        private const string DynastyPropertyName = "__dynasty";
        private const string GenusPropertyName = "__genus";
        private const string NamespacePropertyName = "__namespace";
        private const string PathPropertyName = "__path";
        private const string PropertyCountPropertyName = "__property_count";
        private const string RelpathPropertyName = "__relpath";
        private const string ServerPropertyName = "__server";
        private const string SuperClassPropertyName = "__superclass";

        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        
        private readonly IWbemClassObject _wbemClassObject;

        private bool _disposed;

        internal WindowsManagementObject(IWbemClassObject wmiObject)
        {
            Argument.IsNotNull(() => wmiObject);

            _wbemClassObject = wmiObject;
        }

        public string Class => (string)GetValue(WindowsManagementObject.ClassPropertyName);

        public string[] Derivation => (string[])GetValue(WindowsManagementObject.DerivationPropertyName);

        public string Dynasty => (string)GetValue(WindowsManagementObject.DynastyPropertyName);

        public WmiObjectGenus Genus => (WmiObjectGenus)GetValue(WindowsManagementObject.GenusPropertyName);

        public string Namespace => (string)GetValue(WindowsManagementObject.NamespacePropertyName);

        public string Path => (string)GetValue(WindowsManagementObject.PathPropertyName);

        public int PropertyCount => (int)GetValue(WindowsManagementObject.PropertyCountPropertyName);

        public string Relpath => (string)GetValue(WindowsManagementObject.RelpathPropertyName);

        public string Server => (string)GetValue(WindowsManagementObject.ServerPropertyName);

        public string SuperClass => (string)GetValue(WindowsManagementObject.SuperClassPropertyName);

        public object this[string propertyName]
        {
            get
            {
                return GetValue(propertyName);
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                Marshal.ReleaseComObject(_wbemClassObject);
                _disposed = true;
            }
        }

        public IEnumerable<string> GetPropertyNames()
        {
            ThrowIfDisposed();
            return _wbemClassObject.GetNames();
        }

        public object GetValue(string propertyName)
        {
            ThrowIfDisposed();
            return _wbemClassObject.Get(propertyName);
        }

        public TValue GetValue<TValue>(string propertyName)
        {
             return (TValue)GetValue(propertyName);
        }

        public TValue GetValue<TValue>(string propertyName, Func<object, TValue> converterFunc)
        {
            var finalValue = default(TValue);

            try
            {
                var value = GetValue(propertyName);
                if (value is not null)
                {
                    finalValue = converterFunc(value);
                }
            }
            catch (Exception)
            {
                // Ignore
            }

            return finalValue;
        }

        public override string ToString()
        {
            return Path ?? Class ?? string.Empty;
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw Log.ErrorAndCreateException<ObjectDisposedException>(typeof(WindowsManagementObject).FullName);
            }
        }
    }
}
