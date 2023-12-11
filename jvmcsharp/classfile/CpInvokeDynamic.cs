namespace jvmcsharp.classfile
{
    internal class ConstantMethodTypeInfo : ConstantInfo
    {
        public ushort DescriptorIndex { get; internal set; }

        public void ReadInfo(ClassReader reader)
        {
            DescriptorIndex = reader.ReadUInt16();
        }
    }

    internal class ConstantMethodHandleInfo : ConstantInfo
    {
        public byte ReferenceKind { get; internal set; }
        public ushort ReferenceIndex { get; internal set; }

        public void ReadInfo(ClassReader reader)
        {
            ReferenceKind = reader.ReadUInt8();
            ReferenceIndex = reader.ReadUInt16();
        }
    }

    internal class ConstantInvokeDynamicInfo : ConstantInfo
    {
        public ushort BootstrapMethodAttrIndex { get; internal set; }
        public ushort NameAndTypeIndex { get; internal set; }

        public void ReadInfo(ClassReader reader)
        {
            BootstrapMethodAttrIndex = reader.ReadUInt16();
            NameAndTypeIndex = reader.ReadUInt16();
        }
    }
}
