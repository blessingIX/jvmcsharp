namespace jvmcsharp.classfile
{
    internal class ConstantClassInfo : ConstantInfo
    {
        public ConstantPool Cp { get; set; } = new();
        public ushort NameIndex { get; internal set; }

        public void ReadInfo(ClassReader reader)
        {
            NameIndex = reader.ReadUInt16();
        }

        public string Name() => Cp.GetUtf8(NameIndex);

        public override string ToString() => Name();
    }
}
