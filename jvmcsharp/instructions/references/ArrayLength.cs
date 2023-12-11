using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;

namespace jvmcsharp.instructions.references
{
    internal class ARRAY_LENGTH : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var arrRef = stack.Pop<JavaObject>()
                ?? throw new Exception("java.lang.NullPointException");
            var arrLen = ((ArrayObject)arrRef).ArrayLength();
            stack.Push(arrLen);
        }
    }
}
