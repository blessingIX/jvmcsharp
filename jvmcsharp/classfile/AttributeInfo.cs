namespace jvmcsharp.classfile
{
    internal interface AttributeInfo
    {
        public void ReadInfo(ClassReader reader);

        public static AttributeInfo[] ReadAttributes(ClassReader reader, ConstantPool cp)
        {
            var attributeCount = reader.ReadUInt16();
            var attributes = new AttributeInfo[attributeCount];
            for (int i = 0; i < attributes.Length; i++)
            {
                attributes[i] = ReadAttribute(reader, cp);
            }
            return attributes;
        }

        public static AttributeInfo ReadAttribute(ClassReader reader, ConstantPool cp)
        {
            var attrNameIndex = reader.ReadUInt16();
            var attrName = cp.GetUtf8(attrNameIndex);
            var attrLen = reader.ReadUInt32();
            var attrInfo = Create(attrName, attrLen, cp);
            attrInfo.ReadInfo(reader);
            return attrInfo;
        }

        public static AttributeInfo Create(string attrName, uint attrLen, ConstantPool cp)
        {
            return attrName switch
            {
                "Code" => new CodeAttribute() { Cp = cp },
                "ConstantValue" => new ConstantValueAttribute(),
                "Deprecated" => new DeprecatedAttribute(),
                "Exceptions" => new ExceptionsAttribute(),
                "LineNumberTable" => new LineNumberTableAttribute(),
                "LocalVariableTable" => new LocalVariableTableAttribute(),
                "SourceFile" => new SourceFileAttribute() { Cp = cp },
                "Synthetic" => new SyntheticAttribute(),
                _ => new UnparsedAttribute() { Name = attrName, Length = attrLen },
            };
        }
    }
}
