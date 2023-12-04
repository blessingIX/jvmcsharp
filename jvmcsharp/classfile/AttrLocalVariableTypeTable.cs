namespace jvmcsharp.classfile
{
    internal class LocalVariableTypeTableAttribute : AttributeInfo
    {
        public LocalVariableTypeTableEntry[] LocalVariableTypeTable { get; internal set; } = [];

        public void ReadInfo(ClassReader reader)
        {
            var localVariableTableLength = reader.ReadUInt16();
            LocalVariableTypeTable = new LocalVariableTypeTableEntry[localVariableTableLength];
            for (int i = 0; i < LocalVariableTypeTable.Length; i++)
            {
                LocalVariableTypeTable[i] = new LocalVariableTypeTableEntry()
                {
                    StartPc = reader.ReadUInt16(),
                    Length = reader.ReadUInt16(),
                    NameIndex = reader.ReadUInt16(),
                    SignatureIndex = reader.ReadUInt16(),
                    Index = reader.ReadUInt16(),
                };
            }
        }
    }

    internal class LocalVariableTypeTableEntry
    {
        public ushort StartPc { get; internal set; }
        public ushort Length { get; internal set; }
        public ushort NameIndex { get; internal set; }
        public ushort SignatureIndex { get; internal set; }
        public ushort Index { get; internal set; }
    }
}
