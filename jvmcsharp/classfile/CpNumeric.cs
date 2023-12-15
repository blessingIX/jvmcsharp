namespace jvmcsharp.classfile
{
    internal class ConstantIntegerInfo : ConstantInfo
    {
        public int Val { get; internal set; }

        public void ReadInfo(ClassReader reader)
        {
            var bytes = reader.ReadUInt32();
            Val = (int)bytes;
        }
    }

    internal class ConstantFloatInfo : ConstantInfo
    {
        public float Val { get; internal set; }

        public void ReadInfo(ClassReader reader)
        {
            var bytes = reader.ReadUInt32();
            Val = BitConverter.UInt32BitsToSingle(bytes);
        }
    }

    internal class ConstantLongInfo : ConstantInfo
    {
        public long Val { get; internal set; }

        public void ReadInfo(ClassReader reader)
        {
            var bytes = reader.ReadUInt64();
            Val = (long)bytes;
        }
    }

    internal class ConstantDoubleInfo : ConstantInfo
    {
        public double Val { get; internal set; }

        public void ReadInfo(ClassReader reader)
        {
            var bytes = reader.ReadUInt64();
            Val = BitConverter.UInt64BitsToDouble(bytes);
        }
    }
}
