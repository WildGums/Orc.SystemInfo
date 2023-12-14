namespace Orc.SystemInfo;

public interface IWindowsManagementInformationService
{
    string GetIdentifier(string wmiClass, string wmiProperty);
    string GetIdentifier(string wmiClass, string wmiProperty, string? additionalWmiToCheck, string? additionalWmiToCheckValue);
}