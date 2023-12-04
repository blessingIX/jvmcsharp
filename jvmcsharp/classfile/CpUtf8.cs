using System.Text;

namespace jvmcsharp.classfile
{
    internal class ConstantUtf8Info : ConstantInfo
    {
        public string Str { get; internal set; } = string.Empty;

        public void ReadInfo(ClassReader reader)
        {
            var bytes = reader.ReadBytes(reader.ReadUInt16());
            Str = Encoding.UTF8.GetString(bytes);   // Java中使用的MUTF8编码，与UTF8相似但不兼容，这里简化为UTF8
        }

        public override string ToString() => Str;
    }
}
