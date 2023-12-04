namespace jvmcsharp.classfile
{
    internal class SourceFileAttribute : AttributeInfo
    {
        public ConstantPool Cp { get; internal set; } = new();
        public ushort SourceFileIndex { get; internal set; }

        public void ReadInfo(ClassReader reader)
        {
            SourceFileIndex = reader.ReadUInt16();
        }

        public string FileName() => Cp.GetUtf8(SourceFileIndex);
    }
}
