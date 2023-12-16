namespace jvmcsharp.classfile
{
    internal class CodeAttribute : AttributeInfo
    {
        public ConstantPool Cp { get; internal set; } = new();
        public ushort MaxStack { get; internal set; }
        public ushort MaxLocals { get; internal set; }
        public byte[] Code { get; internal set; } = [];
        public ExceptionTableEntry[] ExceptionTable { get; internal set; } = [];
        public AttributeInfo[] Attributes { get; internal set; } = [];

        public void ReadInfo(ClassReader reader)
        {
            MaxStack = reader.ReadUInt16();
            MaxLocals = reader.ReadUInt16();
            var codeLength = reader.ReadUInt32();
            Code = reader.ReadBytes(codeLength);
            ExceptionTable = ExceptionTableEntry.ReadExceptionTable(reader);
            Attributes = AttributeInfo.ReadAttributes(reader, Cp);
        }

        public LineNumberTableAttribute LineNumberTableAttribute()
        {
            foreach (var attr in Attributes)
            {
                if (attr is LineNumberTableAttribute lineNumberTableAttribute)
                {
                    return lineNumberTableAttribute;
                }
            }
            return null!;
        }
    }

    internal class ExceptionTableEntry
    {
        public ushort StartPc { get; internal set; }
        public ushort EndPc { get; internal set; }
        public ushort HandlePc { get; internal set; }
        public ushort CathcType { get; internal set; }

        public static ExceptionTableEntry[] ReadExceptionTable(ClassReader reader)
        {
            var exceptionTableLength = reader.ReadUInt16();
            var exceptionTable = new ExceptionTableEntry[exceptionTableLength];
            for (int i = 0; i < exceptionTable.Length; i++)
            {
                exceptionTable[i] = new ExceptionTableEntry()
                {
                    StartPc = reader.ReadUInt16(),
                    EndPc = reader.ReadUInt16(),
                    HandlePc = reader.ReadUInt16(),
                    CathcType = reader.ReadUInt16(),
                };
            }
            return exceptionTable;
        }
    }
}
