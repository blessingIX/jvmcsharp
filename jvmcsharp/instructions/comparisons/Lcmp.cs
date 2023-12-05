using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.comparisons
{
    internal class LCMP : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var v2 = stack.Pop<long>();
            var v1 = stack.Pop<long>();
            if (v1 > v2)
            {
                stack.Push(1);
            }
            else if (v1 == v2)
            {
                stack.Push(0);
            }
            else
            {
                stack.Push(-1);
            }
        }
    }
}
