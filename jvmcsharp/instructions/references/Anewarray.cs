using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;

namespace jvmcsharp.instructions.references
{
    internal class ANEW_ARRAY : Index16Instruction
    {
        public override void Execute(Frame frame)
        {
            var cp = frame.Method.Class!.ConstantPool;
            var classRef = cp.Get<ClassRef>(Index);
            var componentClass = classRef.ResolveClass();
            var stack = frame.OperandStack;
            var count = stack.Pop<int>();
            if (count < 0)
            {
                throw new Exception("java.lang.NegativeArraySizeException");
            }
            var arrClass = componentClass.ArrayClass();
            var arr = arrClass.NewArray((uint)count);
            stack.Push<JavaObject>(arr);
        }
    }
}
