using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.math
{
    internal class DNEG : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var val = stack.Pop<double>();
            stack.Push(-val);
        }
    }

    internal class FNEG : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var val = stack.Pop<float>();
            stack.Push(-val);
        }
    }

    internal class INEG : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var val = stack.Pop<int>();
            stack.Push(-val);
        }
    }

    internal class LNEG : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var val = stack.Pop<long>();
            stack.Push(-val);
        }
    }
}
