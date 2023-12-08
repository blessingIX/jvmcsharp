using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;

namespace jvmcsharp.instructions.references
{
    internal class CHECK_CAST : Index16Instruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var @ref = stack.Pop<JavaObject>();
            stack.Push(@ref);
            if (@ref == null)
            {
                return;
            }
            var cp = frame.Method.Class!.ConstantPool;
            var classRef = cp.Get<ClassRef>(Index);
            var @class = classRef.ResolveClass();
            if (!@ref.IsInstanceOf(@class))
            {
                throw new Exception("java.lang.ClassCastException");
            }
        }
    }
}
