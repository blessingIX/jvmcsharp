using jvmcsharp.rtda;

namespace jvmcsharp.instructions.basis
{
    internal abstract class Instruction
    {
        public abstract void FetchOperands(BytecodeReader reader);

        public abstract void Execute(Frame frame);
    }

    /// <summary>
    /// 无操作数指令
    /// </summary>
    internal class NoOperandsInstruction : Instruction
    {
        public override sealed void FetchOperands(BytecodeReader reader)
        {
            // 无操作数指令不需要获取操作数
        }

        public override void Execute(Frame frame)
        {

        }
    }

    internal class BranchInstruction : Instruction
    {
        public int Offset { get; internal set; }

        public override void FetchOperands(BytecodeReader reader) => Offset = reader.ReadInt16();

        public override void Execute(Frame frame)
        {

        }
    }

    internal class Index8Instruction : Instruction
    {
        public uint Index { get; internal set; }

        public override void FetchOperands(BytecodeReader reader) => Index = reader.ReadUInt8();

        public override void Execute(Frame frame)
        {

        }
    }

    internal class Index16Instruction : Instruction
    {
        public uint Index { get; internal set; }

        public override void FetchOperands(BytecodeReader reader) => Index = reader.ReadUInt16();

        public override void Execute(Frame frame)
        {

        }
    }
}
