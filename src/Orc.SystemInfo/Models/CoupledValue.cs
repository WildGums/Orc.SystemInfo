﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CoupledValue.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.SystemInfo.Models
{
    using System;

    [Serializable]
    public class CoupledValue<T1, T2>
    {
        public CoupledValue()
        {
        }

        public CoupledValue(T1 value1, T2 value2)
        {
            Value1 = value1;
            Value2 = value2;
        }

        public T1 Value1 { get; set; }
        public T2 Value2 { get; set; }
    }
}