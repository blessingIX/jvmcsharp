using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;

namespace jvmcsharp.instructions.references
{
    internal class INVOKE_SPECIAL : Index16Instruction
    {
        public override void Execute(Frame frame)
        {
            frame.OperandStack.Pop<JavaObject>();
        }
    }
}
