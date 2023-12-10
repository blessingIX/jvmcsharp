using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.control
{
    internal class TABLE_SWITCH : Instruction
    {
        public int DefaultOffset { get; internal set; }
        public int Low { get; internal set; }
        public int High { get; internal set; }
        public int[] JumpOffsets { get; internal set; } = [];

        public override void Execute(Frame frame)
        {
            var index = frame.OperandStack.Pop<int>();
            int offset = DefaultOffset;
            if (index >= Low && index <= High)
            {
                offset = JumpOffsets[index - Low];
            }
            CommonLogic.Branch(frame, offset);
        }

        public override void FetchOperands(BytecodeReader reader)
        {
            reader.SkipPadding();
            DefaultOffset = reader.ReadInt32();
            Low = reader.ReadInt32();
            High = reader.ReadInt32();
            var jumpOffsetsCount = High - Low + 1;
            JumpOffsets = reader.ReadInt32s(jumpOffsetsCount);
        }
    }
}
