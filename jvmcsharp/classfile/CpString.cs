namespace jvmcsharp.classfile
{
    internal class ConstantStringInfo : ConstantInfo
    {
        public ConstantPool Cp { get; set; } = new();
        public ushort StringIndex { get; internal set; }

        public void ReadInfo(ClassReader reader)
        {
            StringIndex = reader.ReadUInt16();
        }

        public override string ToString() => Cp.GetUtf8(StringIndex);
    }
}
