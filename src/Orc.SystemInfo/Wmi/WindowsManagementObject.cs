namespace Orc.SystemInfo.Wmi;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using Catel;
using Orc.SystemInfo.Win32;

/// <summary>
/// Represent object bound to wbem object
/// </summary>
public class WindowsManagementObject : Disposable
{
    private const string ClassPropertyName = "__CLASS";
    private const string DerivationPropertyName = "__DERIVATION";
    private const string DynastyPropertyName = "__DYNASTY";
    private const string GenusPropertyName = "__GENUS";
    private const string NamespacePropertyName = "__NAMESPACE";
    private const string PathPropertyName = "__PATH";
    private const string PropertyCountPropertyName = "__PROPERTY_COUNT";
    private const string RelpathPropertyName = "__RELPATH";
    private const string ServerPropertyName = "__SERVER";
    private const string SuperClassPropertyName = "__SUPERCLASS";

    private readonly ManagementBaseObject _managementObject;

    internal WindowsManagementObject(ManagementBaseObject managementObject)
    {
        ArgumentNullException.ThrowIfNull(managementObject);

        _managementObject = managementObject;
    }

    public string? Class => (string?)GetValue(ClassPropertyName);

    public string[]? Derivation => (string[]?)GetValue(DerivationPropertyName);

    public string? Dynasty => (string?)GetValue(DynastyPropertyName);

    public WmiObjectGenus? Genus => (WmiObjectGenus?)GetValue(GenusPropertyName);

    public string? Namespace => (string?)GetValue(NamespacePropertyName);

    public string? Path => (string?)GetValue(PathPropertyName);

    public int? PropertyCount => (int?)GetValue(PropertyCountPropertyName);

    public string? Relpath => (string?)GetValue(RelpathPropertyName);

    public string? Server => (string?)GetValue(ServerPropertyName);

    public string? SuperClass => (string?)GetValue(SuperClassPropertyName);

    public object? this[string propertyName]
    {
        get
        {
            return GetValue(propertyName);
        }
    }

    protected override void DisposeManaged()
    {
        base.DisposeManaged();

#pragma warning disable IDISP007 // Don't dispose injected
        _managementObject.Dispose();
#pragma warning restore IDISP007 // Don't dispose injected
    }

    public IEnumerable<string> GetPropertyNames()
    {
        CheckDisposed();
        return _managementObject.Properties.Cast<PropertyData>().Select(p => p.Name);
    }

    public object? GetValue(string propertyName)
    {
        CheckDisposed();
        try
        {
            return _managementObject[propertyName];
        }
        catch
        {
            return null;
        }
    }

    public TValue? GetValue<TValue>(string propertyName)
    {
        return (TValue?)GetValue(propertyName);
    }

    public TValue? GetValue<TValue>(string propertyName, Func<object, TValue> converterFunc)
    {
        var finalValue = default(TValue?);

        try
        {
            var value = GetValue(propertyName);
            if (value is not null)
            {
                finalValue = converterFunc(value);
            }
        }
        catch (Exception)
        {
            // Ignore
        }

        return finalValue;
    }

    public override string ToString()
    {
        return Path ?? Class ?? string.Empty;
    }
}
