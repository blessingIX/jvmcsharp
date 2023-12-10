using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;

namespace jvmcsharp.instructions.references
{
    internal class INVOKE_SPECIAL : Index16Instruction
    {
        public override void Execute(Frame frame)
        {
            var currentClass = frame.Method.Class!;
            var cp = currentClass.ConstantPool;
            var methodRef = cp.Get<MethodRef>(Index);
            var resolvedClass = methodRef.ResolveClass();
            var resolvedMethod = methodRef.ResolveMethod();
            if (resolvedMethod.Name == "<init>" && resolvedMethod.Class != resolvedClass)
            {
                throw new Exception("java.lang.NoSuchMethodError");
            }
            if (resolvedMethod.IsStatic())
            {
                throw new Exception("java.lang.IncompatibleClassChangeError");
            }
            var @ref = frame.OperandStack.GetRefFromTop(resolvedMethod.ArgSlotCount - 1)
                ?? throw new Exception("java.lang.NullPointException");
            if (resolvedMethod.IsProtected()
                && resolvedMethod.Class!.IsSuperClassOf(currentClass)
                && resolvedMethod.Class!.GetPackageName() != currentClass.GetPackageName()
                && @ref.Class != currentClass
                && !@ref.Class.IsSubClassOf(currentClass))
            {
                throw new Exception("java.lang.IllegalAccessError");
            }

            var methodToBeInvoked = resolvedMethod;
            if (currentClass.IsSuper()
                && resolvedClass.IsSuperClassOf(currentClass)
                && resolvedMethod.Name != "<init>")
            {
                methodToBeInvoked = MethodRef.LookupMethodInClass(currentClass.SuperClass!, methodRef.Name, methodRef.Descriptor);
            }

            if (methodToBeInvoked == null || methodToBeInvoked.IsAbstract())
            {
                throw new Exception("java.lang.AbstractMethodError");
            }
            CommonLogic.InvokeMethod(frame, methodToBeInvoked);
        }
    }
}
