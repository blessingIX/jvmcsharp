using jvmcsharp.classfile;

namespace jvmcsharp.rtda.heap
{
    internal class MethodRef : MemberRef
    {
        public MethodRef(ConstantPool cp, ConstantMethodrefInfo refInfo) : base(refInfo)
        {
            Cp = cp;
        }
    }
}
