using jvmcsharp.rtda;

namespace jvmcsharp.instructions.basis
{
    internal class BranchLogic
    {
        public static void Branch(Frame frame, int offset)
        {
            var pc = frame.Thread.Pc;
            var nextPc = pc + offset;
            frame.NextPc = nextPc;
        }
    }
}
