using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.stores
{
    internal class FSTORE : Index8Instruction
    {
        public static void Store(Frame frame, uint index)
        {
            var val = frame.OperandStack.Pop<float>();
            frame.LocalVars.Set(index, val);
        }

        public override void Execute(Frame frame) => Store(frame, Index);
    }

    internal class FSTORE_0 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => FSTORE.Store(frame, 0);
    }

    internal class FSTORE_1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => FSTORE.Store(frame, 1);
    }

    internal class FSTORE_2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => FSTORE.Store(frame, 2);
    }

    internal class FSTORE_3 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => FSTORE.Store(frame, 3);
    }
}
