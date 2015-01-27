// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Pair.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.SystemInfo
{
    using System;

    [Serializable]
    public class SystemInfoElement
    {
        public SystemInfoElement()
        {
        }

        public SystemInfoElement(string value1, string value2)
        {
            Value1 = value1;
            Value2 = value2;
        }

        public string Value1 { get; set; }
        public string Value2 { get; set; }
    }
}