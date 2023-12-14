using jvmcsharp.instructions.basis;
using jvmcsharp.instructions.references;
using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;

namespace jvmcsharp.instructions.constants
{
    internal class LDC : Index8Instruction
    {
        public static void Ldc(Frame frame, uint index)
        {
            var stack = frame.OperandStack;
            var @class = frame.Method.Class!;
            var cp = @class.ConstantPool;
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
                var internedString = StringPool.JavaString(@class.Loader!, stringVal);
                stack.Push(internedString);
            }
            else if (c is ClassRef classRef)
            {
                var classObj = classRef.ResolveClass().JClass;
                stack.Push(classObj);
            }
            else
            {
                throw new Exception("todo: ldc!");
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
