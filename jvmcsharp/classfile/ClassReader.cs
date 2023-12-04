namespace jvmcsharp.classfile
{
    internal class ClassReader(byte[] data)
    {
        public byte[] Data { get; internal set; } = data;

        public byte ReadUInt8()
        {
            var val = Data[0];
            Data = Data[1..^0];
            return val;
        }

        public ushort ReadUInt16()
        {
            var val = BigEndian.ToUInt16(Data);
            Data = Data[2..^0];
            return val;
        }

        public uint ReadUInt32()
        {
            var val = BigEndian.ToUInt32(Data);
            Data = Data[4..^0];
            return val;
        }

        public ulong ReadUInt64()
        {
            var val = BigEndian.ToUInt64(Data);
            Data = Data[8..^0];
            return val;
        }

        public ushort[] ReadUInt16s()
        {
            var n = ReadUInt16();
            var s = new ushort[n];
            for (int i = 0; i < s.Length; i++)
            {
                s[i] = ReadUInt16();
            }
            return s;
        }

        public byte[] ReadBytes(uint len)
        {
            int n = (int)len;
            var bytes = Data[0..n];
            Data = Data[n..^0];
            return bytes;
        }
    }

    internal static class BigEndian
    {
        public static ushort ToUInt16(byte[] data)
        {
            var bytes = BitConverter.GetBytes(BitConverter.ToUInt16(data));
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            return BitConverter.ToUInt16(bytes);
        }

        public static uint ToUInt32(byte[] data)
        {
            var bytes = BitConverter.GetBytes(BitConverter.ToUInt32(data));
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            return BitConverter.ToUInt32(bytes);
        }

        public static ulong ToUInt64(byte[] data)
        {
            var bytes = BitConverter.GetBytes(BitConverter.ToUInt64(data));
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            return BitConverter.ToUInt64(bytes);
        }
    }
}
