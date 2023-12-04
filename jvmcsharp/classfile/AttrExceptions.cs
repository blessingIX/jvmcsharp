namespace jvmcsharp.classfile
{
    internal class ExceptionsAttribute : AttributeInfo
    {
        public ushort[] ExceptionIndexTable { get; internal set; } = [];

        public void ReadInfo(ClassReader reader)
        {
            ExceptionIndexTable = reader.ReadUInt16s();
        }
    }
}
