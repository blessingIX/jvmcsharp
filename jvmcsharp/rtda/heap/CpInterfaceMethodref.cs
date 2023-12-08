using jvmcsharp.classfile;

namespace jvmcsharp.rtda.heap
{
    internal class InterfaceMethodRef : MemberRef
    {
        public InterfaceMethodRef(ConstantPool cp, ConstantInterfaceMethodrefInfo refInfo) : base(refInfo)
        {
            Cp = cp;
        }
    }
}
