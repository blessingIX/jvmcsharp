using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.stores
{
    internal class ISTORE : Index8Instruction
    {
        public static void Store(Frame frame, uint index)
        {
            var val = frame.OperandStack.Pop<int>();
            frame.LocalVars.Set(index, val);
        }

        public override void Execute(Frame frame) => Store(frame, Index);
    }

    internal class ISTORE_0 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => ISTORE.Store(frame, 0);
    }

    internal class ISTORE_1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => ISTORE.Store(frame, 1);
    }

    internal class ISTORE_2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => ISTORE.Store(frame, 2);
    }

    internal class ISTORE_3 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => ISTORE.Store(frame, 3);
    }
}
