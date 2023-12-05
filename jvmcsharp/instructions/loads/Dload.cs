using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.loads
{
    internal class DLOAD : Index8Instruction
    {
        public static void Load(Frame frame, uint index)
        {
            var val = frame.LocalVars.Get<double>(index);
            frame.OperandStack.Push(val);
        }

        public override void Execute(Frame frame) => Load(frame, Index);
    }

    internal class DLOAD_0 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => DLOAD.Load(frame, 0);
    }

    internal class DLOAD_1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => DLOAD.Load(frame, 1);
    }

    internal class DLOAD_2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => DLOAD.Load(frame, 2);
    }

    internal class DLOAD_3 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => DLOAD.Load(frame, 3);
    }
}
