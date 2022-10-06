namespace Orc.SystemInfo.Wmi
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Orc.SystemInfo.Win32;

    public class WindowsManagementQuery : IEnumerable<WindowsManagementObject?>
    {
        private readonly string _wql;
        private readonly WindowsManagementConnection _connection;
        private readonly WbemClassObjectEnumeratorBehaviorOptions _enumeratorBehaviorOptions;

        public WindowsManagementQuery(WindowsManagementConnection connection, string wql, 
            WbemClassObjectEnumeratorBehaviorOptions enumeratorBehaviorOptions = WbemClassObjectEnumeratorBehaviorOptions.ReturnImmediately)
        {
            ArgumentNullException.ThrowIfNull(connection);
            ArgumentNullException.ThrowIfNull(wql);

            _wql = wql;
            _connection = connection;
            _enumeratorBehaviorOptions = enumeratorBehaviorOptions;
        }

        public string Wql => _wql;

        public WindowsManagementConnection Connection => _connection;

        public WbemClassObjectEnumeratorBehaviorOptions EnumeratorBehaviorOption => _enumeratorBehaviorOptions;

        public IEnumerator<WindowsManagementObject?> GetEnumerator()
        {
            var enumerator = _connection.ExecuteQuery(this);
            return enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
