using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.math
{
    /// <summary>
    /// Shift left int
    /// </summary>
    internal class ISHL : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var v2 = stack.Pop<int>();
            var v1 = stack.Pop<int>();
            stack.Push(v1 << v2);
        }
    }

    /// <summary>
    /// Arithmetic shift right int
    /// </summary>
    internal class ISHR : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var v2 = stack.Pop<int>();
            var v1 = stack.Pop<int>();
            stack.Push(v1 >> v2);
        }
    }

    /// <summary>
    /// Logic shift right int
    /// </summary>
    internal class IUSHR : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var v2 = stack.Pop<int>();
            var v1 = stack.Pop<int>();
            stack.Push(v1 >>> v2);
        }
    }

    /// <summary>
    /// Shift left long
    /// </summary>
    internal class LSHL : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var v2 = stack.Pop<int>();
            var v1 = stack.Pop<long>();
            stack.Push(v1 << v2);
        }
    }

    /// <summary>
    /// Arithmetic shift right long
    /// </summary>
    internal class LSHR : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var v2 = stack.Pop<int>();
            var v1 = stack.Pop<long>();
            stack.Push(v1 >> v2);
        }
    }

    /// <summary>
    /// Logic shift right long
    /// </summary>
    internal class LUSHR : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var v2 = stack.Pop<int>();
            var v1 = stack.Pop<long>();
            stack.Push(v1 >>> v2);
        }
    }
}
