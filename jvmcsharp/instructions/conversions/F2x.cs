using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.conversions
{
    internal class F2D : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var d = stack.Pop<float>();
            var val = Convert.ToDouble(d);
            stack.Push(val);
        }
    }

    internal class F2I : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var d = stack.Pop<float>();
            var val = Convert.ToInt32(d);
            stack.Push(val);
        }
    }

    internal class F2L : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var d = stack.Pop<float>();
            var val = Convert.ToInt64(d);
            stack.Push(val);
        }
    }
}
