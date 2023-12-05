using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.loads
{
    internal class ILOAD : Index8Instruction
    {
        public static void Load(Frame frame, uint index)
        {
            var val = frame.LocalVars.Get<int>(index);
            frame.OperandStack.Push(val);
        }

        public override void Execute(Frame frame) => Load(frame, Index);
    }

    internal class ILOAD_0 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => ILOAD.Load(frame, 0);
    }

    internal class ILOAD_1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => ILOAD.Load(frame, 1);
    }

    internal class ILOAD_2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => ILOAD.Load(frame, 2);
    }

    internal class ILOAD_3 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => ILOAD.Load(frame, 3);
    }
}
