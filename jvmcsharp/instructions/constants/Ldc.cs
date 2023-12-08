using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;

namespace jvmcsharp.instructions.constants
{
    internal class LDC : Index8Instruction
    {
        public static void Ldc(Frame frame, uint index)
        {
            var stack = frame.OperandStack;
            var cp = frame.Method.Class!.ConstantPool;
            var c = cp.Get<object>(index);
            if (c is int intVal)
            {
                stack.Push(intVal);
            }
            else if (c is float floatVal)
            {
                stack.Push(floatVal);
            }
            else if (c is string stringVal)
            {
                // TODO
            }
            else if (c is ClassRef classRef)
            {
                // TODO
            }
            else
            {
                throw new NotImplementedException("todo: ldc!");
            }
        }

        public override void Execute(Frame frame) => Ldc(frame, Index);
    }

    internal class LDC_W : Index16Instruction
    {
        public override void Execute(Frame frame) => LDC.Ldc(frame, Index);
    }

    internal class LDC2_W : Index16Instruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var cp = frame.Method.Class!.ConstantPool;
            var c = cp.Get<object>(Index);
            if (c is long longVal)
            {
                stack.Push(longVal);
            }
            else if (c is double doubleVal)
            {
                stack.Push(doubleVal);
            }
            else
            {
                throw new Exception("java.lang.ClassFormatError");
            }
        }
    }
}
