using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.math
{
    internal class IINC : Instruction
    {
        public uint Index { get; internal set; }
        public int Const { get; internal set; }

        public override void Execute(Frame frame)
        {
            var locaVars = frame.LocalVars;
            var val = locaVars.Get<int>(Index);
            val += Const;
            locaVars.Set(Index, val);
        }

        public override void FetchOperands(BytecodeReader reader)
        {
            Index = reader.ReadUInt8();
            Const = reader.ReadInt8();
        }
    }
}
