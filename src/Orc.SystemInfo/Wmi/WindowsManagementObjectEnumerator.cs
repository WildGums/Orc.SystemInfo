namespace Orc.SystemInfo.Wmi;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Management;
using Catel.Reflection;

public sealed class WindowsManagementObjectEnumerator : IEnumerator<WindowsManagementObject?>
{
    private readonly ManagementObjectCollection _collection;

    private ManagementObjectCollection.ManagementObjectEnumerator _enumerator;

    private bool _disposed;

    internal WindowsManagementObjectEnumerator(ManagementObjectCollection collection)
    {
        ArgumentNullException.ThrowIfNull(collection);

        _collection = collection;
        _enumerator = collection.GetEnumerator();
    }

#pragma warning disable IDISP002 // Dispose member
    public WindowsManagementObject? Current { get; private set; }
#pragma warning restore IDISP002 // Dispose member

    object? IEnumerator.Current
    {
        get
        {
            return Current;
        }
    }

    public bool MoveNext()
    {
        ThrowIfDisposed();

        if (!_enumerator.MoveNext())
        {
            return false;
        }

        var managementObject = _enumerator.Current;
        if (managementObject is null)
        {
            return false;
        }

#pragma warning disable IDISP003 // Dispose previous before re-assigning
        Current = new WindowsManagementObject(managementObject);
#pragma warning restore IDISP003 // Dispose previous before re-assigning

        return true;
    }

    public void Reset()
    {
        ThrowIfDisposed();

        _enumerator.Dispose();
        _enumerator = _collection.GetEnumerator();
    }

    /// <summary>
    /// Releases all resources used by the <see cref="WindowsManagementObjectEnumerator"/>.
    /// </summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

#pragma warning disable IDISP007 // Don't dispose injected
        _enumerator.Dispose();
        _collection.Dispose();
#pragma warning restore IDISP007 // Don't dispose injected

        _disposed = true;
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(typeof(WindowsManagementObjectEnumerator).GetSafeFullName());
        }
    }
}
