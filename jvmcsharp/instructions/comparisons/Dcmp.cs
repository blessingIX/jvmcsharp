using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.comparisons
{
    internal class DCMPG : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => Cmp(frame, true);

        public static void Cmp(Frame frame, bool gFlag)
        {
            var stack = frame.OperandStack;
            var v2 = stack.Pop<double>();
            var v1 = stack.Pop<double>();
            if (v1 > v2)
            {
                stack.Push(1);
            }
            else if (v1 == v2)
            {
                stack.Push(0);
            }
            else if (v1 < v2)
            {
                stack.Push(-1);
            }
            else if (gFlag)
            {
                stack.Push(1);
            }
            else
            {
                stack.Push(-1);
            }
        }
    }

    internal class DCMPL : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => DCMPG.Cmp(frame, false);
    }
}
