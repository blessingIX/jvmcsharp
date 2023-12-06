using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.comparisons
{
    internal class FCMPG : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => Cmp(frame, true);

        public static void Cmp(Frame frame, bool gFlag)
        {
            var stack = frame.OperandStack;
            var v2 = stack.Pop<float>();
            var v1 = stack.Pop<float>();
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

    internal class FCMPL : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => FCMPG.Cmp(frame, false);
    }
}
