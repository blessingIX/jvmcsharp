using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.comparisons
{
    internal class IFEQ : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            var val = frame.OperandStack.Pop<int>();
            if (val == 0)
            {
                BranchLogic.Branch(frame, Offset);
            }
        }
    }

    internal class IFNE : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            var val = frame.OperandStack.Pop<int>();
            if (val != 0)
            {
                BranchLogic.Branch(frame, Offset);
            }
        }
    }

    internal class IFLT : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            var val = frame.OperandStack.Pop<int>();
            if (val < 0)
            {
                BranchLogic.Branch(frame, Offset);
            }
        }
    }

    internal class IFLE : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            var val = frame.OperandStack.Pop<int>();
            if (val <= 0)
            {
                BranchLogic.Branch(frame, Offset);
            }
        }
    }

    internal class IFGT : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            var val = frame.OperandStack.Pop<int>();
            if (val > 0)
            {
                BranchLogic.Branch(frame, Offset);
            }
        }
    }

    internal class IFGE : BranchInstruction
    {

        public override void Execute(Frame frame)
        {
            var val = frame.OperandStack.Pop<int>();
            if (val >= 0)
            {
                BranchLogic.Branch(frame, Offset);
            }
        }
    }
}
