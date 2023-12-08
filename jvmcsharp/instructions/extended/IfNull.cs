using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;

namespace jvmcsharp.instructions.extended
{
    internal class IFNULL : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            var @ref = frame.OperandStack.Pop<JavaObject>();
            if (@ref is null)
            {
                BranchLogic.Branch(frame, Offset);
            }
        }
    }

    internal class IFNONNULL : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            var @ref = frame.OperandStack.Pop<JavaObject>();
            if (@ref is not null)
            {
                BranchLogic.Branch(frame, Offset);
            }
        }
    }
}
