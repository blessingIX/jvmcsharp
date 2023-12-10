using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.comparisons
{
    internal class IF_ICMPEQ : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var v2 = stack.Pop<int>();
            var v1 = stack.Pop<int>();
            if (v1 == v2)
            {
                CommonLogic.Branch(frame, Offset);
            }
        }
    }

    internal class IF_ICMPNE : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var v2 = stack.Pop<int>();
            var v1 = stack.Pop<int>();
            if (v1 != v2)
            {
                CommonLogic.Branch(frame, Offset);
            }
        }
    }

    internal class IF_ICMPLT : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var v2 = stack.Pop<int>();
            var v1 = stack.Pop<int>();
            if (v1 < v2)
            {
                CommonLogic.Branch(frame, Offset);
            }
        }
    }

    internal class IF_ICMPLE : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var v2 = stack.Pop<int>();
            var v1 = stack.Pop<int>();
            if (v1 <= v2)
            {
                CommonLogic.Branch(frame, Offset);
            }
        }
    }

    internal class IF_ICMPGT : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var v2 = stack.Pop<int>();
            var v1 = stack.Pop<int>();
            if (v1 > v2)
            {
                CommonLogic.Branch(frame, Offset);
            }
        }
    }

    internal class IF_ICMPGE : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var v2 = stack.Pop<int>();
            var v1 = stack.Pop<int>();
            if (v1 >= v2)
            {
                CommonLogic.Branch(frame, Offset);
            }
        }
    }
}
