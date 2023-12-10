using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.control
{
    internal class LOOKUP_SWITCH : Instruction
    {
        public int DefaultOffset { get; internal set; }
        public int Npairs { get; internal set; }
        public int[] MatchOffsets { get; internal set; } = [];

        public override void Execute(Frame frame)
        {
            var key = frame.OperandStack.Pop<int>();
            var offset = DefaultOffset;
            for (int i = 0; i < Npairs * 2; i += 2)
            {
                if (MatchOffsets[i] == key)
                {
                    offset = MatchOffsets[i + 1];
                    break;
                }
            }
            CommonLogic.Branch(frame, offset);
        }

        public override void FetchOperands(BytecodeReader reader)
        {
            reader.SkipPadding();
            DefaultOffset = reader.ReadInt32();
            Npairs = reader.ReadInt32();
            MatchOffsets = reader.ReadInt32s(Npairs * 2);
        }
    }
}
