namespace Orc.SystemInfo.Wmi
{
    using System.Collections;
    using System.Collections.Generic;
    using Catel;
    using Orc.SystemInfo.Win32;

    public class WindowsManagementQuery : IEnumerable<WmiObject>
    {
        private readonly string _wql;
        private readonly WindowsManagementConnection _connection;
        private readonly WbemClassObjectEnumeratorBehaviorOptions _enumeratorBehaviorOptions;

        public WindowsManagementQuery(WindowsManagementConnection connection, string wql, WbemClassObjectEnumeratorBehaviorOptions enumeratorBehaviorOptions = WbemClassObjectEnumeratorBehaviorOptions.ReturnImmediately)
        {
            Argument.IsNotNull(() => connection);
            Argument.IsNotNull(() => wql);

            _wql = wql;
            _connection = connection;
            _enumeratorBehaviorOptions = enumeratorBehaviorOptions;
        }

        public string Wql => _wql;

        public WindowsManagementConnection Connection => _connection;

        public WbemClassObjectEnumeratorBehaviorOptions EnumeratorBehaviorOption => _enumeratorBehaviorOptions;

        public IEnumerator<WmiObject> GetEnumerator()
        {
            return _connection.ExecuteQuery(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
