namespace System.Xml {
    public struct XMLIRWValue : IDisposable, IEquatable<XMLIRWValue>, IConvertible {
        private object value;

        public static XMLIRWValue Empty => new XMLIRWValue(null);

        public XMLIRWValue(object value) {
            this.value = value;
        }

        public void Dispose() {
            value = null;
        }

        public bool Equals(XMLIRWValue other) => other.value == value;

        public override string ToString()
            => value == (object)null ? string.Empty : value.ToString();

        public string ToString(IFormatProvider provider)
            => Convert.ToString(value, provider);
            
        public override bool Equals(object obj)
            => obj is XMLIRWValue V && Equals(V);

        public override int GetHashCode() => base.GetHashCode();

        TypeCode IConvertible.GetTypeCode()
            => Convert.GetTypeCode(value);

        bool IConvertible.ToBoolean(IFormatProvider provider)
            => Convert.ToBoolean(value, provider);

        byte IConvertible.ToByte(IFormatProvider provider)
            => Convert.ToByte(value, provider);

        char IConvertible.ToChar(IFormatProvider provider)
            => Convert.ToChar(value, provider);

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
            => Convert.ToDateTime(value, provider);

        decimal IConvertible.ToDecimal(IFormatProvider provider)
            => Convert.ToDecimal(value, provider);

        double IConvertible.ToDouble(IFormatProvider provider)
            => Convert.ToDouble(value, provider);

        short IConvertible.ToInt16(IFormatProvider provider)
            => Convert.ToInt16(value, provider);

        int IConvertible.ToInt32(IFormatProvider provider)
            => Convert.ToInt32(value, provider);

        long IConvertible.ToInt64(IFormatProvider provider)
            => Convert.ToInt64(value, provider);

        sbyte IConvertible.ToSByte(IFormatProvider provider)
            => Convert.ToSByte(value, provider);

        float IConvertible.ToSingle(IFormatProvider provider)
            => Convert.ToSingle(value, provider);

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
            => Convert.ChangeType(value, conversionType, provider);

        ushort IConvertible.ToUInt16(IFormatProvider provider)
            => Convert.ToUInt16(value, provider);

        uint IConvertible.ToUInt32(IFormatProvider provider)
            => Convert.ToUInt32(value, provider);

        ulong IConvertible.ToUInt64(IFormatProvider provider)
            => Convert.ToUInt64(value, provider);

        public static bool operator ==(XMLIRWValue left, XMLIRWValue right) => left.Equals(right);
        public static bool operator !=(XMLIRWValue left, XMLIRWValue right) => !(left == right);

        public static explicit operator string(XMLIRWValue V) 
            => V == Empty ? (string)null : Convert.ToString(V);
    }
}