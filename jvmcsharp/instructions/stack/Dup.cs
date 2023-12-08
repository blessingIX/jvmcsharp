using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;

namespace jvmcsharp.instructions.stack
{
    internal class DUP : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var slot = stack.Pop<object>();
            stack.Push(slot);
            stack.Push(slot);
        }
    }

    internal class DUP_X1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var slot1 = stack.Pop<object>();
            var slot2 = stack.Pop<object>();
            stack.Push(slot1);
            stack.Push(slot2);
            stack.Push(slot1);
        }
    }

    internal class DUP_X2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var slot1 = stack.Pop<object>();
            var slot2 = stack.Pop<object>();
            var slot3 = stack.Pop<object>();
            stack.Push(slot1);
            stack.Push(slot3);
            stack.Push(slot2);
            stack.Push(slot1);
        }
    }

    internal class DUP2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var slot1 = stack.Pop<object>();
            var slot2 = stack.Pop<object>();
            stack.Push(slot2);
            stack.Push(slot1);
            stack.Push(slot2);
            stack.Push(slot1);
        }
    }

    internal class DUP2_X1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var slot1 = stack.Pop<object>();
            var slot2 = stack.Pop<object>();
            var slot3 = stack.Pop<object>();
            stack.Push(slot2);
            stack.Push(slot1);
            stack.Push(slot3);
            stack.Push(slot2);
            stack.Push(slot1);
        }
    }

    internal class DUP2_X2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var slot1 = stack.Pop<object>();
            var slot2 = stack.Pop<object>();
            var slot3 = stack.Pop<object>();
            var slot4 = stack.Pop<object>();
            stack.Push(slot2);
            stack.Push(slot1);
            stack.Push(slot4);
            stack.Push(slot3);
            stack.Push(slot2);
            stack.Push(slot1);
        }
    }
}
