using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;

namespace jvmcsharp.instructions.loads
{
    internal class AALOAD : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var index = stack.Pop<int>();
            var arrRef = stack.Pop<ArrayObject>() 
                ?? throw new Exception("java.lang.NullPointException");
            var vals = arrRef.Refs;
            if (index < 0 || index >= vals.Length)
            {
                throw new Exception("ArrayIndexOutOfBoundsException");
            }
            stack.Push(vals[index]);
        }
    }

    internal class BALOAD : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var index = stack.Pop<int>();
            var arrRef = stack.Pop<ArrayObject>()
                ?? throw new Exception("java.lang.NullPointException");
            var vals = arrRef.Bytes;
            if (index < 0 || index >= vals.Length)
            {
                throw new Exception("ArrayIndexOutOfBoundsException");
            }
            stack.Push(vals[index]);
        }
    }

    internal class CALOAD : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var index = stack.Pop<int>();
            var arrRef = stack.Pop<ArrayObject>()
                ?? throw new Exception("java.lang.NullPointException");
            var vals = arrRef.Chars;
            if (index < 0 || index >= vals.Length)
            {
                throw new Exception("ArrayIndexOutOfBoundsException");
            }
            stack.Push(vals[index]);
        }
    }

    internal class DALOAD : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var index = stack.Pop<int>();
            var arrRef = stack.Pop<ArrayObject>()
                ?? throw new Exception("java.lang.NullPointException");
            var vals = arrRef.Doubles;
            if (index < 0 || index >= vals.Length)
            {
                throw new Exception("ArrayIndexOutOfBoundsException");
            }
            stack.Push(vals[index]);
        }
    }

    internal class FALOAD : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var index = stack.Pop<int>();
            var arrRef = stack.Pop<ArrayObject>()
                ?? throw new Exception("java.lang.NullPointException");
            var vals = arrRef.Flotas;
            if (index < 0 || index >= vals.Length)
            {
                throw new Exception("ArrayIndexOutOfBoundsException");
            }
            stack.Push(vals[index]);
        }
    }

    internal class IALOAD : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var index = stack.Pop<int>();
            var arrRef = stack.Pop<ArrayObject>()
                ?? throw new Exception("java.lang.NullPointException");
            var vals = arrRef.Ints;
            if (index < 0 || index >= vals.Length)
            {
                throw new Exception("ArrayIndexOutOfBoundsException");
            }
            stack.Push(vals[index]);
        }
    }

    internal class LALOAD : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var index = stack.Pop<int>();
            var arrRef = stack.Pop<ArrayObject>()
                ?? throw new Exception("java.lang.NullPointException");
            var vals = arrRef.Longs;
            if (index < 0 || index >= vals.Length)
            {
                throw new Exception("ArrayIndexOutOfBoundsException");
            }
            stack.Push(vals[index]);
        }
    }

    internal class SALOAD : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var index = stack.Pop<int>();
            var arrRef = stack.Pop<ArrayObject>()
                ?? throw new Exception("java.lang.NullPointException");
            var vals = arrRef.Shorts;
            if (index < 0 || index >= vals.Length)
            {
                throw new Exception("ArrayIndexOutOfBoundsException");
            }
            stack.Push(vals[index]);
        }
    }
}
