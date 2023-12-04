namespace jvmcsharp.classfile
{
    internal class ConstantValueAttribute : AttributeInfo
    {
        public ushort ConstantValueIndex { get; internal set; }

        public void ReadInfo(ClassReader reader)
        {
            ConstantValueIndex = reader.ReadUInt16();
        }
    }
}
