// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWmiService.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.SystemInfo
{
    public interface IWindowsManagementInformationService
    {
        string GetIdentifier(string wmiClass, string wmiProperty);
        string GetIdentifier(string wmiClass, string wmiProperty, string additionalWmiToCheck, string additionalWmiToCheckValue);
    }
}