using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.conversions
{
    internal class I2B : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var d = stack.Pop<int>();
            var val = Convert.ToByte(d);
            stack.Push(val);
        }
    }

    internal class I2S : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var d = stack.Pop<int>();
            var val = Convert.ToInt16(d);
            stack.Push(val);
        }
    }

    internal class I2C : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var d = stack.Pop<int>();
            var val = Convert.ToChar(d);
            stack.Push(val);
        }
    }

    internal class I2F : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var d = stack.Pop<int>();
            var val = Convert.ToSingle(d);
            stack.Push(val);
        }
    }

    internal class I2L : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var d = stack.Pop<int>();
            var val = Convert.ToInt64(d);
            stack.Push(val);
        }
    }

    internal class I2D : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var d = stack.Pop<int>();
            var val = Convert.ToDouble(d);
            stack.Push(val);
        }
    }
}
