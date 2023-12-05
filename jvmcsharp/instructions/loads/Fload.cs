using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.loads
{
    internal class FLOAD : Index8Instruction
    {
        public static void Load(Frame frame, uint index)
        {
            var val = frame.LocalVars.Get<float>(index);
            frame.OperandStack.Push(val);
        }

        public override void Execute(Frame frame) => Load(frame, Index);
    }

    internal class FLOAD_0 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => FLOAD.Load(frame, 0);
    }

    internal class FLOAD_1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => FLOAD.Load(frame, 1);
    }

    internal class FLOAD_2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => FLOAD.Load(frame, 2);
    }

    internal class FLOAD_3 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => FLOAD.Load(frame, 3);
    }
}
