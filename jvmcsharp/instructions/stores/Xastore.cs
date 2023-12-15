using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;

namespace jvmcsharp.instructions.stores
{
    internal class AASTORE : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var val = stack.Pop<JavaObject>();
            var index = stack.Pop<int>();
            var arrRef = stack.Pop<ArrayObject>()
                ?? throw new Exception("java.lang.NullPointException");
            var vals = arrRef.Refs;
            if (index < 0 || index >= vals.Length)
            {
                throw new Exception("ArrayIndexOutOfBoundsException");
            }
            vals[index] = val;
        }
    }

    internal class BASTORE : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var val = stack.Pop<int>();
            var index = stack.Pop<int>();
            var arrRef = stack.Pop<ArrayObject>()
                ?? throw new Exception("java.lang.NullPointException");
            var vals = arrRef.Bytes;
            if (index < 0 || index >= vals.Length)
            {
                throw new Exception("ArrayIndexOutOfBoundsException");
            }
            vals[index] = (sbyte)val;
        }
    }

    internal class CASTORE : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var val = stack.Pop<int>();
            var index = stack.Pop<int>();
            var arrRef = stack.Pop<ArrayObject>()
                ?? throw new Exception("java.lang.NullPointException");
            var vals = arrRef.Chars;
            if (index < 0 || index >= vals.Length)
            {
                throw new Exception("ArrayIndexOutOfBoundsException");
            }
            vals[index] = (ushort)val;
        }
    }

    internal class DASTORE : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var val = stack.Pop<double>();
            var index = stack.Pop<int>();
            var arrRef = stack.Pop<ArrayObject>()
                ?? throw new Exception("java.lang.NullPointException");
            var vals = arrRef.Doubles;
            if (index < 0 || index >= vals.Length)
            {
                throw new Exception("ArrayIndexOutOfBoundsException");
            }
            vals[index] = val;
        }
    }

    internal class FASTORE : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var val = stack.Pop<float>();
            var index = stack.Pop<int>();
            var arrRef = stack.Pop<ArrayObject>()
                ?? throw new Exception("java.lang.NullPointException");
            var vals = arrRef.Flotas;
            if (index < 0 || index >= vals.Length)
            {
                throw new Exception("ArrayIndexOutOfBoundsException");
            }
            vals[index] = val;
        }
    }

    internal class IASTORE : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var val = stack.Pop<int>();
            var index = stack.Pop<int>();
            var arrRef = stack.Pop<ArrayObject>()
                ?? throw new Exception("java.lang.NullPointException");
            var vals = arrRef.Ints;
            if (index < 0 || index >= vals.Length)
            {
                throw new Exception("ArrayIndexOutOfBoundsException");
            }
            vals[index] = val;
        }
    }

    internal class LASTORE : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var val = stack.Pop<long>();
            var index = stack.Pop<int>();
            var arrRef = stack.Pop<ArrayObject>()
                ?? throw new Exception("java.lang.NullPointException");
            var vals = arrRef.Longs;
            if (index < 0 || index >= vals.Length)
            {
                throw new Exception("ArrayIndexOutOfBoundsException");
            }
            vals[index] = val;
        }
    }

    internal class SASTORE : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var val = stack.Pop<short>();
            var index = stack.Pop<int>();
            var arrRef = stack.Pop<ArrayObject>()
                ?? throw new Exception("java.lang.NullPointException");
            var vals = arrRef.Shorts;
            if (index < 0 || index >= vals.Length)
            {
                throw new Exception("ArrayIndexOutOfBoundsException");
            }
            vals[index] = val;
        }
    }
}
