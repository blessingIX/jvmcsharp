namespace jvmcsharp.classfile
{
    internal class ClassFile
    {
        public uint Magic { get; internal set; }
        public ushort MinorVersion { get; internal set; }
        public ushort MajorVersion { get; internal set; }
        public ConstantPool ConstantPool { get; internal set; } = new();
        public ushort AccessFlags { get; internal set; }
        public ushort ThisClass { get; internal set; }
        public ushort SuperClass { get; internal set; }
        public ushort[] Interfaces { get; internal set; } = [];
        public MemberInfo[] Fileds { get; internal set; } = [];
        public MemberInfo[] Methods { get; internal set; } = [];
        public AttributeInfo[] Attribute { get; internal set; } = [];

        public ClassFile(byte[] classData) => Read(new ClassReader(classData));

        private void Read(ClassReader reader)
        {
            ReadAndCheckMagic(reader);
            ReadAndCheckVersion(reader);
            ConstantPool = ConstantPool.ReadConstantPool(reader);
            AccessFlags = reader.ReadUInt16();
            ThisClass = reader.ReadUInt16();
            SuperClass = reader.ReadUInt16();
            Interfaces = reader.ReadUInt16s();
            Fileds = MemberInfo.ReadMembers(reader, ConstantPool);
            Methods = MemberInfo.ReadMembers(reader, ConstantPool);
            Attribute = AttributeInfo.ReadAttributes(reader, ConstantPool);
        }

        private void ReadAndCheckMagic(ClassReader reader)
        {
            Magic = reader.ReadUInt32();
            if (Magic != 0xCAFEBABE)
            {
                throw new Exception("java.lang.ClassFormatError: magic!");
            }
        }

        private void ReadAndCheckVersion(ClassReader reader)
        {
            MinorVersion = reader.ReadUInt16();
            MajorVersion = reader.ReadUInt16();
            switch (MajorVersion)
            {
                case 45:
                    return;
                case 46:
                case 47:
                case 48:
                case 49:
                case 50:
                case 51:
                case 52:    // 支持到Java8
                    if (MinorVersion == 0)
                    {
                        return;
                    }
                    break;
                default:
                    break;
            }
            throw new Exception("java.lang.UnsupportedClassVersionError!");
        }

        public string ClassName() => ConstantPool.GetClassName(ThisClass);

        public string SuperClassName() => SuperClass > 0 ? ConstantPool.GetClassName(SuperClass) : string.Empty;

        public string[] InterfaceNames() => Interfaces.Select(i => ConstantPool.GetClassName(i)).ToArray();
    }
}
