using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.math
{
    internal class DSUB : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var v2 = stack.Pop<double>();
            var v1 = stack.Pop<double>();
            var result = v1 - v2;
            stack.Push(result);
        }
    }

    internal class FSUB : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var v2 = stack.Pop<float>();
            var v1 = stack.Pop<float>();
            var result = v1 - v2;
            stack.Push(result);
        }
    }

    internal class ISUB : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var v2 = stack.Pop<int>();
            var v1 = stack.Pop<int>();
            var result = v1 - v2;
            stack.Push(result);
        }
    }

    internal class LSUB : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var v2 = stack.Pop<long>();
            var v1 = stack.Pop<long>();
            var result = v1 - v2;
            stack.Push(result);
        }
    }
}
