using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.comparisons
{
    internal class IF_ACMPEQ : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var v2 = stack.Pop<object>();
            var v1 = stack.Pop<object>();
            if (ReferenceEquals(v1, v2))
            {
                BranchLogic.Branch(frame, Offset);
            }
        }
    }

    internal class IF_ACMPNE : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var v2 = stack.Pop<object>();
            var v1 = stack.Pop<object>();
            if (!ReferenceEquals(v1, v2))
            {
                BranchLogic.Branch(frame, Offset);
            }
        }
    }
}
