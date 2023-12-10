using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;

namespace jvmcsharp.instructions.comparisons
{
    internal class IF_ACMPEQ : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var v2 = stack.Pop<JavaObject>();
            var v1 = stack.Pop<JavaObject>();
            if (ReferenceEquals(v1, v2))
            {
                CommonLogic.Branch(frame, Offset);
            }
        }
    }

    internal class IF_ACMPNE : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var v2 = stack.Pop<JavaObject>();
            var v1 = stack.Pop<JavaObject>();
            if (!ReferenceEquals(v1, v2))
            {
                CommonLogic.Branch(frame, Offset);
            }
        }
    }
}
