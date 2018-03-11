// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DotNetFrameworkService.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.SystemInfo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Catel;
    using Catel.Logging;
    using Microsoft.Win32;

    public class DotNetFrameworkService : IDotNetFrameworkService
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public virtual IEnumerable<string> GetInstalledFrameworks()
        {
            return GetNetFrameworkVersions();
        }

        protected IEnumerable<string> GetNetFrameworkVersions()
        {
            var versions = new List<string>();

            throw Log.ErrorAndCreateException<PlatformNotSupportedException>(string.Empty);

            return versions;
        }
    }
}