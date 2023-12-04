namespace jvmcsharp.classfile
{
    internal class UnparsedAttribute : AttributeInfo
    {
        public string Name { get; internal set; } = string.Empty;
        public uint Length { get; internal set; }
        public byte[] Info { get; internal set; } = [];

        public void ReadInfo(ClassReader reader)
        {
            Info = reader.ReadBytes(Length);
        }
    }
}
