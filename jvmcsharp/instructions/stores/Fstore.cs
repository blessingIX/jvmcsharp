using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.stores
{
    internal class FSTORE : Index8Instruction
    {
        public static void Fstore(Frame frame, uint index)
        {
            var val = frame.OperandStack.Pop<float>();
            frame.LocalVars.Set(index, val);
        }

        public override void Execute(Frame frame) => Fstore(frame, Index);
    }

    internal class FSTORE_0 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => FSTORE.Fstore(frame, 0);
    }

    internal class FSTORE_1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => FSTORE.Fstore(frame, 1);
    }

    internal class FSTORE_2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => FSTORE.Fstore(frame, 2);
    }

    internal class FSTORE_3 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => FSTORE.Fstore(frame, 3);
    }
}
