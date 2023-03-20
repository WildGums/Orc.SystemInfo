namespace Orc.SystemInfo.Wmi;

using System;
using System.Collections;
using System.Collections.Generic;
using Win32;

public class WindowsManagementQuery : IEnumerable<WindowsManagementObject?>
{
    private readonly string _wql;
    private readonly WbemClassObjectEnumeratorBehaviorOptions _enumeratorBehaviorOptions;

    public WindowsManagementQuery(WindowsManagementConnection connection, string wql, 
        WbemClassObjectEnumeratorBehaviorOptions enumeratorBehaviorOptions = WbemClassObjectEnumeratorBehaviorOptions.ReturnImmediately)
    {
        ArgumentNullException.ThrowIfNull(connection);
        ArgumentNullException.ThrowIfNull(wql);

        _wql = wql;
        Connection = connection;
        _enumeratorBehaviorOptions = enumeratorBehaviorOptions;
    }

    public string Wql => _wql;

    public WindowsManagementConnection Connection { get; }

    public WbemClassObjectEnumeratorBehaviorOptions EnumeratorBehaviorOption => _enumeratorBehaviorOptions;

    public IEnumerator<WindowsManagementObject?> GetEnumerator()
    {
        var enumerator = Connection.ExecuteQuery(this);
        return enumerator;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
