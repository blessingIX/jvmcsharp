using jvmcsharp.classfile;

namespace jvmcsharp.rtda.heap
{
    internal class MemberRef : SymRef
    {
        public string Name { get; internal set; }
        public string Descriptor { get; internal set; }

        public MemberRef(ConstantMemberrefInfo refInfo)
        {
            ClassName = refInfo.ClassName();
            (Name, Descriptor) = refInfo.NameAndDescriptor();
        }
    }
}
