namespace Orc.SystemInfo.Wmi
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using Catel;
    using Catel.Logging;
    using Orc.SystemInfo.Win32;

    public sealed class WindowsManagementObjectEnumerator : IEnumerator<WindowsManagementObject>
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly IWbemClassObjectEnumerator _wbemClassObjectEnumerator;

        private bool _disposed;

        internal WindowsManagementObjectEnumerator(IWbemClassObjectEnumerator enumerator)
        {
            Argument.IsNotNull(() => enumerator);

            _wbemClassObjectEnumerator = enumerator;

            enumerator.Reset();
        }

        public WindowsManagementObject Current { get; private set; }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public bool MoveNext()
        {
            ThrowIfDisposed();

            IWbemClassObject currentWmiObject = _wbemClassObjectEnumerator.Next();
            if (currentWmiObject is null)
            {
                return false;
            }

            Current = new WindowsManagementObject(currentWmiObject);
            return true;
        }

        public void Reset()
        {
            ThrowIfDisposed();

            HResult hresult = _wbemClassObjectEnumerator.Reset();
            if (hresult.Failed)
            {
                throw (Exception)hresult;
            }
        }

        /// <summary>
        /// Releases all resources used by the <see cref="WindowsManagementObjectEnumerator"/>.
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                Marshal.ReleaseComObject(_wbemClassObjectEnumerator);

                _disposed = true;
            }
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw Log.ErrorAndCreateException<ObjectDisposedException>(typeof(WindowsManagementObjectEnumerator).FullName);
            }
        }
    }
}
