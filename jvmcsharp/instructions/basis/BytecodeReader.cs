namespace jvmcsharp.instructions.basis
{
    internal class BytecodeReader
    {
        public byte[] Code { get; internal set; } = [];
        public int Pc { get; internal set; }

        public void Reset(byte[] code, int pc)
        {
            Code = code;
            Pc = pc;
        }

        public sbyte ReadInt8() => (sbyte)ReadUInt8();

        public byte ReadUInt8()
        {
            var i = Code[Pc];
            Pc++;
            return i;
        }

        public short ReadInt16() => (short)ReadUInt16();

        public ushort ReadUInt16()
        {
            var byte1 = ReadUInt8();
            var byte2 = ReadUInt8();
            return (ushort)(byte1 << 8 | byte2);
        }

        public int ReadInt32()
        {
            var byte1 = ReadUInt8();
            var byte2 = ReadUInt8();
            var byte3 = ReadUInt8();
            var byte4 = ReadUInt8();
            return (byte1 << 24) | (byte2 << 16) | (byte3 << 8) | byte4;
        }

        public void SkipPadding()
        {
            while (Pc % 4 != 0)
            {
                ReadUInt8();
            }
        }

        public int[] ReadInt32s(int n)
        {
            var ints = new int[n];
            for (int i = 0; i < ints.Length; i++)
            {
                ints[i] = ReadInt32();
            }
            return ints;
        }
    }
}
