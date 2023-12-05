using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.loads
{
    internal class ALOAD : Index8Instruction
    {
        public static void Load(Frame frame, uint index)
        {
            var val = frame.LocalVars.Get<object>(index);
            frame.OperandStack.Push(val);
        }

        public override void Execute(Frame frame) => Load(frame, Index);
    }

    internal class ALOAD_0 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => ALOAD.Load(frame, 0);
    }

    internal class ALOAD_1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => ALOAD.Load(frame, 1);
    }

    internal class ALOAD_2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => ALOAD.Load(frame, 2);
    }

    internal class ALOAD_3 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => ALOAD.Load(frame, 3);
    }
}
