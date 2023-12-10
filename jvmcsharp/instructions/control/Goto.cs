using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.control
{
    internal class GOTO : BranchInstruction
    {
        public override void Execute(Frame frame) => CommonLogic.Branch(frame, Offset);
    }
}
