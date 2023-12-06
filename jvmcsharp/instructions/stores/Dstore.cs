using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.stores
{
    internal class DSTORE : Index8Instruction
    {
        public static void Store(Frame frame, uint index)
        {
            var val = frame.OperandStack.Pop<double>();
            frame.LocalVars.Set(index, val);
        }

        public override void Execute(Frame frame) => Store(frame, Index);
    }

    internal class DSTORE_0 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => DSTORE.Store(frame, 0);
    }

    internal class DSTORE_1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => DSTORE.Store(frame, 1);
    }

    internal class DSTORE_2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => DSTORE.Store(frame, 2);
    }

    internal class DSTORE_3 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => DSTORE.Store(frame, 3);
    }
}
