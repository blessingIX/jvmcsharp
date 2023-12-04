namespace jvmcsharp.classfile
{
    internal abstract class AbstractConstantMemberrefInfo : ConstantInfo
    {
        public abstract string ClassName();
        public abstract (string, string) NameAndDescriptor();
        public abstract void ReadInfo(ClassReader reader);
    }

    internal class ConstantMemberrefInfo : AbstractConstantMemberrefInfo
    {
        public ConstantPool Cp { get; set; } = new();
        public ushort ClassIndex { get; set; }
        public ushort NameAndTypeIndex { get; set; }

        public override void ReadInfo(ClassReader reader)
        {
            ClassIndex = reader.ReadUInt16();
            NameAndTypeIndex = reader.ReadUInt16();
        }

        public override string ClassName() => Cp.GetClassName(ClassIndex);

        public override (string, string) NameAndDescriptor() => Cp.GetNameAndType(NameAndTypeIndex);
    }

    internal class ConstantFieldrefInfo : ConstantMemberrefInfo { }

    internal class ConstantMethodrefInfo : ConstantMemberrefInfo { }

    internal class ConstantInterfaceMethodrefInfo : ConstantMemberrefInfo { }
}
