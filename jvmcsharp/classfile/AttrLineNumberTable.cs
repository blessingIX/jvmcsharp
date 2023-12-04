namespace jvmcsharp.classfile
{
    internal class LineNumberTableAttribute : AttributeInfo
    {
        public LineNumberTableEntry[] LineNumberTable { get; internal set; } = [];

        public void ReadInfo(ClassReader reader)
        {
            var lineNumberTableLength = reader.ReadUInt16();
            LineNumberTable = new LineNumberTableEntry[lineNumberTableLength];
            for (int i = 0; i < LineNumberTable.Length; i++)
            {
                LineNumberTable[i] = new LineNumberTableEntry()
                {
                    StartPc = reader.ReadUInt16(),
                    LineNumber = reader.ReadUInt16(),
                };
            }
        }
    }

    internal class LineNumberTableEntry
    {
        public ushort StartPc { get; internal set; }
        public ushort LineNumber { get; internal set; }
    }
}
