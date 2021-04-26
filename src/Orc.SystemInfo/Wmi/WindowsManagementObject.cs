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
    public class WmiObject : IDisposable
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

        internal WmiObject(IWbemClassObject wmiObject)
        {
            Argument.IsNotNull(() => wmiObject);

            _wbemClassObject = wmiObject;
        }

        public string Class => (string)GetPropertyValue(WmiObject.ClassPropertyName);

        public string[] Derivation => (string[])GetPropertyValue(WmiObject.DerivationPropertyName);

        public string Dynasty => (string)GetPropertyValue(WmiObject.DynastyPropertyName);

        public WmiObjectGenus Genus => (WmiObjectGenus)GetPropertyValue(WmiObject.GenusPropertyName);

        public string Namespace => (string)GetPropertyValue(WmiObject.NamespacePropertyName);

        public string Path => (string)GetPropertyValue(WmiObject.PathPropertyName);

        public int PropertyCount => (int)GetPropertyValue(WmiObject.PropertyCountPropertyName);

        public string Relpath => (string)GetPropertyValue(WmiObject.RelpathPropertyName);

        public string Server => (string)GetPropertyValue(WmiObject.ServerPropertyName);

        public string SuperClass => (string)GetPropertyValue(WmiObject.SuperClassPropertyName);

        public object this[string propertyName]
        {
            get
            {
                return GetPropertyValue(propertyName);
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

        public object GetPropertyValue(string propertyName)
        {
            ThrowIfDisposed();
            return _wbemClassObject.Get(propertyName);
        }

        public TResult GetPropertyValue<TResult>(string propertyName)
        {
            return (TResult)GetPropertyValue(propertyName);
        }

        public override string ToString()
        {
            return Path ?? Class ?? string.Empty;
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw Log.ErrorAndCreateException<ObjectDisposedException>(typeof(WmiObject).FullName);
            }
        }
    }
}
