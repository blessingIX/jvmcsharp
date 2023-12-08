using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;

namespace jvmcsharp.instructions.references
{
    internal class INSTANCE_OF : Index16Instruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var @ref = stack.Pop<JavaObject>();
            if (@ref == null)
            {
                stack.Push(0);
                return;
            }
            var cp = frame.Method.Class!.ConstantPool;
            var classRef = cp.Get<ClassRef>(Index);
            var @class = classRef.ResolveClass();
            stack.Push(@ref.IsInstanceOf(@class) ? 1 : 0);
        }
    }
}
