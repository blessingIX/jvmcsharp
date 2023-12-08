using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;

namespace jvmcsharp.instructions.constants
{
    internal class ACONST_NULL : NoOperandsInstruction  
    {
        public override void Execute(Frame frame) => frame.OperandStack.Push<JavaObject>(null!);
    }

    internal class DCONST_0 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => frame.OperandStack.Push(0d);
    }

    internal class DCONST_1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => frame.OperandStack.Push(1d);
    }

    internal class FCONST_0 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => frame.OperandStack.Push(0f);
    }

    internal class FCONST_1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => frame.OperandStack.Push(1f);
    }

    internal class FCONST_2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => frame.OperandStack.Push(2f);
    }

    internal class ICONST_M1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => frame.OperandStack.Push(-1);
    }

    internal class ICONST_0 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => frame.OperandStack.Push(0);
    }

    internal class ICONST_1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => frame.OperandStack.Push(1);
    }

    internal class ICONST_2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => frame.OperandStack.Push(2);
    }

    internal class ICONST_3 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => frame.OperandStack.Push(3);
    }

    internal class ICONST_4 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => frame.OperandStack.Push(4);
    }

    internal class ICONST_5 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => frame.OperandStack.Push(5);
    }

    internal class LCONST_0 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => frame.OperandStack.Push(0L);
    }

    internal class LCONST_1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => frame.OperandStack.Push(1L);
    }
}
