using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.stack
{
    internal class SWAP : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var slot1 = stack.Pop<object>();
            var slot2 = stack.Pop<object>();
            stack.Push(slot1);
            stack.Push(slot2);
        }
    }
}
