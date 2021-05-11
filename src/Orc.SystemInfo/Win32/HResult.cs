namespace Orc.SystemInfo.Win32
{
    using System;
    using System.Runtime.InteropServices;

    internal struct HResult : IEquatable<HResult>
    {
        private readonly int _value;

        public HResult(int value)
        {
            _value = value;
        }

        public bool Failed => _value < 0;

        public static implicit operator Exception(HResult hr)
        {
            if (hr.Failed)
            {
                return Marshal.GetExceptionForHR(hr._value) ?? new COMException("Unknown exception", hr._value);
            }

            return null;
        }

        public static implicit operator int(HResult hr)
        {
            return hr._value;
        }

        public static implicit operator HResult(int intValue)
        {
            return new HResult(intValue);
        }

        public static bool operator ==(HResult x, HResult y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(HResult x, HResult y)
        {
            return !x.Equals(y);
        }

        public static bool operator ==(HResult x, Enum y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(HResult x, Enum y)
        {
            return !x.Equals(y);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (obj is not HResult result)
            {
                if (obj is int intVal)
                {
                    result = intVal;
                }
                else
                {
                    return false;
                }
            }

            return Equals(result);
        }

        public bool Equals(HResult hr)
        {
            return _value == hr._value;
        }
    }
}
