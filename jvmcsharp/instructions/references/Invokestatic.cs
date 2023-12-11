using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;

namespace jvmcsharp.instructions.references
{
    internal class INVOKE_STATIC : Index16Instruction
    {
        public override void Execute(Frame frame)
        {
            Class @class = frame.Method.Class!;
            var cp = @class.ConstantPool;
            var methodRef = cp.Get<MethodRef>(Index);
            var resolvedMethod = methodRef.ResolveMethod();
            if (!@class.InitStarted)
            {
                frame.RevertNextPc();
                CommonLogic.InitClass(frame.Thread, @class);
                return;
            }
            if (!resolvedMethod.IsStatic())
            {
                throw new Exception("java.lang.IncompatibleClassChangeError");
            }
            CommonLogic.InvokeMethod(frame, resolvedMethod);
        }
    }
}
