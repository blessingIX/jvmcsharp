namespace jvmcsharp.classfile
{
    internal class ConstantNameAndTypeInfo : ConstantInfo
    {
        public ushort NameIndex { get; internal set; }
        public ushort DescriptorIndex { get; internal set; }

        public void ReadInfo(ClassReader reader)
        {
            NameIndex = reader.ReadUInt16();
            DescriptorIndex = reader.ReadUInt16();
        }
    }
}
