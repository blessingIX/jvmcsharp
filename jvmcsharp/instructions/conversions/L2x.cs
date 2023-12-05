using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.conversions
{
    internal class L2I : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var d = stack.Pop<long>();
            var val = Convert.ToInt32(d);
            stack.Push(val);
        }
    }

    internal class L2F : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var d = stack.Pop<long>();
            var val = Convert.ToSingle(d);
            stack.Push(val);
        }
    }

    internal class L2D : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var d = stack.Pop<long>();
            var val = Convert.ToDouble(d);
            stack.Push(val);
        }
    }
}
