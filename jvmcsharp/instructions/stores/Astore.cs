using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.stores
{
    internal class ASTORE : Index8Instruction
    {
        public static void Astore(Frame frame, uint index)
        {
            var @ref = frame.OperandStack.Pop<object>();
            frame.LocalVars.Set(index, @ref);
        }

        public override void Execute(Frame frame) => Astore(frame, Index);
    }

    internal class ASTORE_0 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => ASTORE.Astore(frame, 0);
    }

    internal class ASTORE_1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => ASTORE.Astore(frame, 1);
    }

    internal class ASTORE_2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => ASTORE.Astore(frame, 2);
    }

    internal class ASTORE_3 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => ASTORE.Astore(frame, 3);
    }
}
