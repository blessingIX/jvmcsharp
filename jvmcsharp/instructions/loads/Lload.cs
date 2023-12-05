using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.loads
{
    internal class LLOAD : Index8Instruction
    {
        public static void Load(Frame frame, uint index)
        {
            var val = frame.LocalVars.Get<long>(index);
            frame.OperandStack.Push(val);
        }

        public override void Execute(Frame frame) => Load(frame, Index);
    }

    internal class LLOAD_0 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => LLOAD.Load(frame, 0);
    }

    internal class LLOAD_1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => LLOAD.Load(frame, 1);
    }

    internal class LLOAD_2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => LLOAD.Load(frame, 2);
    }

    internal class LLOAD_3 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => LLOAD.Load(frame, 3);
    }
}
