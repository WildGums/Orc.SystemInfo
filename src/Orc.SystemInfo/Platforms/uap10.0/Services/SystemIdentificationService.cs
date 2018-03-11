// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemIdentificationService.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.SystemInfo
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Text;

    using Catel;
    using Catel.Caching;
    using Catel.Logging;
    using Catel.Threading;
    using MethodTimer;

    public class SystemIdentificationService : ISystemIdentificationService
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        
        public SystemIdentificationService()
        {

        }

        [Time]
        public virtual string GetMachineId(string separator = "-", bool hashCombination = true)
        {
            Argument.IsNotNull(() => separator);

            throw Log.ErrorAndCreateException<PlatformNotSupportedException>(string.Empty);
        }

        [Time]
        public virtual string GetMacId()
        {
            throw Log.ErrorAndCreateException<PlatformNotSupportedException>(string.Empty);
        }

        [Time]
        public virtual string GetGpuId()
        {
            throw Log.ErrorAndCreateException<PlatformNotSupportedException>(string.Empty);
        }

        [Time]
        public virtual string GetHardDriveId()
        {
            throw Log.ErrorAndCreateException<PlatformNotSupportedException>(string.Empty);
        }

        [Time]
        public virtual string GetMotherboardId()
        {
            throw Log.ErrorAndCreateException<PlatformNotSupportedException>(string.Empty);
        }

        [Time]
        public virtual string GetCpuId()
        {
            throw Log.ErrorAndCreateException<PlatformNotSupportedException>(string.Empty);
        }
    }
}