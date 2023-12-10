using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;

namespace jvmcsharp.instructions.references
{
    internal class INVOKE_STATIC : Index16Instruction
    {
        public override void Execute(Frame frame)
        {
            var cp = frame.Method.Class!.ConstantPool;
            var methodRef = cp.Get<MethodRef>(Index);
            var resolvedMethod = methodRef.ResolveMethod();
            if (!resolvedMethod.IsStatic())
            {
                throw new Exception("java.lang.IncompatibleClassChangeError");
            }
            CommonLogic.InvokeMethod(frame, resolvedMethod);
        }
    }
}
