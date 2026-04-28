namespace Orc.SystemInfo.Wmi;

using System;
using System.Runtime.InteropServices;
using Catel;
using Catel.Logging;
using Microsoft.Extensions.Logging;
using Orc.SystemInfo.Win32;

public sealed class WindowsManagementConnection : Disposable
{
    private const string DefaultLocalRootPath = @"\\.\root\cimv2";

    private readonly object _lock = new();

    private readonly ILogger _logger;

    private bool _connected;
    private IWbemServices? _wbemServices;
    private IWbemContext? _context;

    public WindowsManagementConnection(ILogger logger)
    {
        _logger = logger;
    }

    public void Open()
    {
        try
        {
            CheckDisposed();

            if (_connected)
            {
                return;
            }

            var locator = new WbemLocator();

            lock (_lock)
            {
                if (_connected)
                {
                    return;
                }

                const WbemAuthenticationLevel authLevel = WbemAuthenticationLevel.PacketIntegrity;

                _wbemServices = locator.ConnectServer(DefaultLocalRootPath, _context);

                if (_wbemServices is not null)
                {
                    _wbemServices.SetProxy(WbemImpersonationLevel.Impersonate, authLevel);

                    _connected = true;
                }
            }
        }
        catch (Exception ex)
        {
            _context = null;
            _logger.LogError(ex, "Failed to open the connection");
        }
    }

    protected override void DisposeUnmanaged()
    {
        base.DisposeUnmanaged();

        lock (_lock)
        {
            if (_wbemServices is not null)
            {
                if (Marshal.IsComObject(_wbemServices))
                {
                    Marshal.ReleaseComObject(_wbemServices);
                }

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
            throw _logger.LogErrorAndCreateException<InvalidOperationException>("Cannot execute query without services");
        }

        var enumerator = wbemServices.ExecQuery(query.Wql, query.EnumeratorBehaviorOption, _context);
        return enumerator;
    }
}
