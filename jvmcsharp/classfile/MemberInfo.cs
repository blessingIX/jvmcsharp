namespace jvmcsharp.classfile
{
    internal class MemberInfo
    {
        public ConstantPool Cp { get; set; } = new();
        public ushort AccessFlags { get; set; }
        public ushort NameIndex { get; set; }
        public ushort DescriptorIndex { get; set; }
        public AttributeInfo[] Attributes { get; set; } = [];

        public static MemberInfo[] ReadMembers(ClassReader reader, ConstantPool cp)
        {
            var memberCount = reader.ReadUInt16();
            var members = new MemberInfo[memberCount];
            for (int i = 0; i < members.Length; i++)
            {
                members[i] = ReadMember(reader, cp);
            }
            return members;
        }

        public static MemberInfo ReadMember(ClassReader reader, ConstantPool cp)
        {
            return new()
            {
                Cp = cp,
                AccessFlags = reader.ReadUInt16(),
                NameIndex = reader.ReadUInt16(),
                DescriptorIndex = reader.ReadUInt16(),
                Attributes = AttributeInfo.ReadAttributes(reader, cp),
            };
        }

        public string Name() => Cp.GetUtf8(NameIndex);

        public string Descriptor() => Cp.GetUtf8(DescriptorIndex);

        public CodeAttribute CodeAttribute()
        {
            foreach (var member in Attributes)
            {
                if (member is CodeAttribute codeAttribute)
                    return codeAttribute;
            }
            return null!;
        }

        public ConstantValueAttribute ConstantValueAttribute()
        {
            foreach (var attrInfo in Attributes)
            {
                if (attrInfo is ConstantValueAttribute constantValueAttribute)
                {
                    return constantValueAttribute;
                }
            }
            return null!;
        }
    }
}
