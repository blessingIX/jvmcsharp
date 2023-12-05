using jvmcsharp.instructions.basis;
using jvmcsharp.instructions.control;

namespace jvmcsharp.instructions.extended
{
    internal class GOTO_W : GOTO
    {
        public override void FetchOperands(BytecodeReader reader) => Offset = reader.ReadInt32();
    }
}
