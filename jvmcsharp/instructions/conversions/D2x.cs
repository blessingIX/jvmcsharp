using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.conversions
{
    internal class D2F : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var d = stack.Pop<double>();
            var val = Convert.ToSingle(d);
            stack.Push(val);
        }
    }

    internal class D2I : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var d = stack.Pop<double>();
            var val = Convert.ToInt32(d);
            stack.Push(val);
        }
    }

    internal class D2L : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var d = stack.Pop<double>();
            var val = Convert.ToInt64(d);
            stack.Push(val);
        }
    }
}
