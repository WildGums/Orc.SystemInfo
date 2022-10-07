namespace Orc.SystemInfo.Wmi
{
    using System;
    using System.Runtime.InteropServices;
    using Catel;
    using Catel.Logging;
    using Orc.SystemInfo.Win32;

    public sealed class WindowsManagementConnection : Disposable
    {
        private const string DefaultLocalRootPath = @"\\.\root\cimv2";

        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly object _lock = new();

        private bool _connected;
        private IWbemServices? _wbemServices;
        private IWbemContext? _context = null;

        public void Open()
        {
            try
            {
                CheckDisposed();

                if (!_connected)
                {
                    var locator = new WbemLocator();

                    lock (_lock)
                    {
                        if (!_connected)
                        {
                            var authLevel = WbemAuthenticationLevel.PacketIntegrity;

                            _wbemServices = locator.ConnectServer(DefaultLocalRootPath, _context);
                            _wbemServices.SetProxy(WbemImpersonationLevel.Impersonate, authLevel);

                            _connected = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _context = null;
                Log.Error(ex);
            }
        }

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            lock (_lock)
            {
                if (_wbemServices is not null)
                {
                    Marshal.ReleaseComObject(_wbemServices);
                    _wbemServices = null;
                }

                _connected = false;
            }
        }

        public WindowsManagementQuery CreateQuery(string wql)
        {
            ArgumentNullException.ThrowIfNull(wql);

            return new WindowsManagementQuery(this, wql);
        }

        public WindowsManagementObjectEnumerator ExecuteQuery(WindowsManagementQuery query)
        {
            ArgumentNullException.ThrowIfNull(query);

            return new WindowsManagementObjectEnumerator(InternalExecuteQuery(query));
        }

        internal IWbemClassObjectEnumerator InternalExecuteQuery(WindowsManagementQuery query)
        {
            CheckDisposed();
            Open();

            var wbemServices = _wbemServices;
            if (wbemServices is null)
            {
                throw Log.ErrorAndCreateException<InvalidOperationException>("Cannot execute query without services");
            }

            var enumerator = wbemServices.ExecQuery(query.Wql, query.EnumeratorBehaviorOption, _context);
            return enumerator;
        }
    }
}
