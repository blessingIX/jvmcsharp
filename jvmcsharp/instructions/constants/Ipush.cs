using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.constants
{
    internal class BIPUSH : Instruction
    {
        public byte Val { get; internal set; }

        public override void Execute(Frame frame)
        {
            var i = (int)Val;
            frame.OperandStack.Push(i);
        }

        public override void FetchOperands(BytecodeReader reader) => Val = reader.ReadInt8();
    }

    internal class SIPUSH : Instruction
    {
        public short Val { get; internal set; }

        public override void Execute(Frame frame)
        {
            var i = (int)Val;
            frame.OperandStack.Push(i);
        }

        public override void FetchOperands(BytecodeReader reader) => Val = reader.ReadInt16();
    }
}
