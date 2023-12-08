using jvmcsharp.classfile;

namespace jvmcsharp.rtda.heap
{
    internal class ClassRef : SymRef
    {
        public ClassRef(ConstantPool cp, ConstantClassInfo classInfo)
        {
            Cp = cp;
            ClassName = classInfo.Name();
        }
    }
}
