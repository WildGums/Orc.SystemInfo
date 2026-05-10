namespace Orc.SystemInfo.Wmi;

using System;
using System.Management;
using Catel;
using Catel.Logging;
using Microsoft.Extensions.Logging;

public sealed class WindowsManagementConnection : Disposable
{
    private const string DefaultLocalRootPath = @"\\.\root\cimv2";

    private readonly object _lock = new();

    private readonly ILogger _logger;

    private bool _connected;
    private ManagementScope? _managementScope;

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

            lock (_lock)
            {
                if (_connected)
                {
                    return;
                }

                var options = new ConnectionOptions
                {
                    Impersonation = ImpersonationLevel.Impersonate,
                    Authentication = AuthenticationLevel.PacketIntegrity,
                    EnablePrivileges = true
                };

                _managementScope = new ManagementScope(DefaultLocalRootPath, options);
                _managementScope.Connect();

                _connected = true;
            }
        }
        catch (Exception ex)
        {
            _managementScope = null;
            _logger.LogError(ex, "Failed to open the connection");
        }
    }

    protected override void DisposeManaged()
    {
        base.DisposeManaged();

        lock (_lock)
        {
            _managementScope = null;
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

    internal ManagementObjectCollection InternalExecuteQuery(WindowsManagementQuery query)
    {
        CheckDisposed();
        Open();

        var scope = _managementScope;
        if (scope is null)
        {
            throw _logger.LogErrorAndCreateException<InvalidOperationException>("Cannot execute query without management scope");
        }

        var objectQuery = new ObjectQuery(query.Wql);
        using var searcher = new ManagementObjectSearcher(scope, objectQuery);
        return searcher.Get();
    }
}
