using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.stores
{
    internal class LSTORE : Index8Instruction
    {
        public static void Store(Frame frame, uint index)
        {
            var val = frame.OperandStack.Pop<long>();
            frame.LocalVars.Set(index, val);
        }

        public override void Execute(Frame frame) => Store(frame, Index);
    }

    internal class LSTORE_0 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => LSTORE.Store(frame, 0);
    }

    internal class LSTORE_1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => LSTORE.Store(frame, 1);
    }

    internal class LSTORE_2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => LSTORE.Store(frame, 2);
    }

    internal class LSTORE_3 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => LSTORE.Store(frame, 3);
    }
}
