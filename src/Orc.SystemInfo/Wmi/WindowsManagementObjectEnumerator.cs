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

#pragma warning disable IDISP002 // Dispose member
        public WindowsManagementObject Current { get; private set; }
#pragma warning restore IDISP002 // Dispose member

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

            var currentWmiObject = _wbemClassObjectEnumerator.Next();
            if (currentWmiObject is null)
            {
                return false;
            }

#pragma warning disable IDISP003 // Dispose previous before re-assigning
            Current = new WindowsManagementObject(currentWmiObject);
#pragma warning restore IDISP003 // Dispose previous before re-assigning
            return true;
        }

        public void Reset()
        {
            ThrowIfDisposed();

            var hresult = _wbemClassObjectEnumerator.Reset();
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
