namespace jvmcsharp.classfile
{
    internal class LocalVariableTableAttribute : AttributeInfo
    {
        public LocalVariableTableEntry[] LocalVariableTable { get; internal set; } = [];

        public void ReadInfo(ClassReader reader)
        {
            var localVariableTableLength = reader.ReadUInt16();
            LocalVariableTable = new LocalVariableTableEntry[localVariableTableLength];
            for (int i = 0; i < LocalVariableTable.Length; i++)
            {
                LocalVariableTable[i] = new LocalVariableTableEntry()
                {
                    StartPc = reader.ReadUInt16(),
                    Length = reader.ReadUInt16(),
                    NameIndex = reader.ReadUInt16(),
                    DescriptorIndex = reader.ReadUInt16(),
                    Index = reader.ReadUInt16(),
                };
            }
        }
    }

    internal class LocalVariableTableEntry
    {
        public ushort StartPc { get; internal set; }
        public ushort Length { get; internal set; }
        public ushort NameIndex { get; internal set; }
        public ushort DescriptorIndex { get; internal set; }
        public ushort Index { get; internal set; }
    }
}
