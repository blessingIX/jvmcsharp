using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.math
{
    internal class DDIV : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var v2 = stack.Pop<double>();
            var v1 = stack.Pop<double>();
            var result = v1 / v2;
            stack.Push(result);
        }
    }

    internal class FDIV : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var v2 = stack.Pop<float>();
            var v1 = stack.Pop<float>();
            var result = v1 / v2;
            stack.Push(result);
        }
    }

    internal class IDIV : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var v2 = stack.Pop<int>();
            var v1 = stack.Pop<int>();
            if (v2 == 0)
            {
                throw new Exception("java.lang.ArithmeticException: / by zero");
            }
            var result = v1 / v2;
            stack.Push(result);
        }
    }

    internal class LDIV : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var v2 = stack.Pop<long>();
            var v1 = stack.Pop<long>();
            if (v2 == 0)
            {
                throw new Exception("java.lang.ArithmeticException: / by zero");
            }
            var result = v1 / v2;
            stack.Push(result);
        }
    }
}
