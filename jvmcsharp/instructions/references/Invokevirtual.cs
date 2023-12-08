using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;

namespace jvmcsharp.instructions.references
{
    internal class INVOKE_VIRTUAL : Index16Instruction
    {
        public override void Execute(Frame frame)
        {
            var cp = frame.Method.Class!.ConstantPool;
            var methodRef = cp.Get<MethodRef>(Index);
            if (methodRef.Name == "println")
            {
                var stack = frame.OperandStack;
                switch (methodRef.Descriptor)
                {
                    case "(Z)V":
                        Console.WriteLine(stack.Pop<int>() != 0);
                        break;
                    case "(C)V":
                        Console.WriteLine(stack.Pop<int>());
                        break;
                    case "(B)V":
                        Console.WriteLine(stack.Pop<int>());
                        break;
                    case "(S)V":
                        Console.WriteLine(stack.Pop<int>());
                        break;
                    case "(I)V":
                        Console.WriteLine(stack.Pop<int>());
                        break;
                    case "(J)V":
                        Console.WriteLine(stack.Pop<long>());
                        break;
                    case "(F)V":
                        Console.WriteLine(stack.Pop<float>());
                        break;
                    case "(D)V":
                        Console.WriteLine(stack.Pop<double>());
                        break;
                }
            }
        }
    }
}
