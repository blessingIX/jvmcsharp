namespace jvmcsharp.classfile
{
    internal class ConstantPool
    {
        private ConstantInfo[] ConstantInfos { get; set; } = [];

        public int Length => ConstantInfos.Length;

        public ConstantInfo? this[int idx]
        {
            get => (idx >= 0 && idx < ConstantInfos.Length) ? ConstantInfos[idx] : null;
            set => ConstantInfos[idx] = value!;
        }

        public static ConstantPool ReadConstantPool(ClassReader reader)
        {
            var cpCount = (int)reader.ReadUInt16();
            var cp = new ConstantPool() { ConstantInfos = new ConstantInfo[cpCount] };
            for (int i = 1; i < cpCount; i++)
            {
                cp[i] = ConstantInfo.ReadConstantInfo(reader, cp);
                if (cp[i] is ConstantLongInfo || cp[i] is ConstantDoubleInfo)
                {
                    i++;
                }
            }
            return cp;
        }

        private ConstantInfo GetConstantInfo(ushort index)
        {
            var cpInfo = ConstantInfos[index];
            if (cpInfo != null)
            {
                return cpInfo;
            }
            throw new Exception("Invalid constant pool index!");
        }

        public (string, string) GetNameAndType(ushort index)
        {
            var info = GetConstantInfo(index);
            if (info is ConstantNameAndTypeInfo ntInfo)
            {
                var name = GetUtf8(ntInfo.NameIndex);
                var type = GetUtf8(ntInfo.DescriptorIndex);
                return (name, type);
            }
            return (string.Empty, string.Empty);
        }

        public string GetClassName(ushort index)
        {
            var info = GetConstantInfo(index);
            if (info is ConstantClassInfo classInfo)
            {
                return GetUtf8(classInfo.NameIndex);
            }
            return string.Empty;
        }

        public string GetUtf8(ushort index)
        {
            var info = GetConstantInfo(index);
            if (info is ConstantUtf8Info utf8Info)
            {
                return utf8Info.ToString() ?? string.Empty;
            }
            return string.Empty;
        }
    }
}
