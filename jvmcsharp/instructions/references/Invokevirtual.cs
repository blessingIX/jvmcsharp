using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;

namespace jvmcsharp.instructions.references
{
    internal class INVOKE_VIRTUAL : Index16Instruction
    {
        public override void Execute(Frame frame)
        {
            var currentClass = frame.Method.Class!;
            var cp = currentClass.ConstantPool;
            var methodRef = cp.Get<MethodRef>(Index);
            var resolvedMethod = methodRef.ResolveMethod();
            if (resolvedMethod.IsStatic())
            {
                throw new Exception("java.lang.IncompatiblClassChangeeError");
            }
            var @ref = frame.OperandStack.GetRefFromTop(resolvedMethod.ArgSlotCount - 1);
            // 在java.lang.System中
            // public final static PrintStream out = null;
            // out属性被final修饰且为null
            if (@ref == null)
            {
                // Syste.out.println();
                // java.io.PrintStream#println(int)
                if (methodRef.Name == "println")
                {
                    PrintLn(frame, methodRef);
                    return;
                }
                throw new Exception("java.lang.NullPointException");
            }

            if (resolvedMethod.IsProtected()
                && resolvedMethod.Class!.IsSuperClassOf(currentClass)
                && resolvedMethod.Class!.GetPackageName() != currentClass.GetPackageName()
                && @ref.Class != currentClass
                && !@ref.Class.IsSubClassOf(currentClass))
            {
                throw new Exception("java.lang.IllegalAccessError");
            }

            var methodToBeInvoked = MethodRef.LookupMethodInClass(@ref.Class, methodRef.Name, methodRef.Descriptor);
            if (methodToBeInvoked == null || methodToBeInvoked.IsAbstract())
            {
                throw new Exception("java.lang.AbstarctMethodError");
            }
            CommonLogic.InvokeMethod(frame, methodToBeInvoked);
        }

        private static void PrintLn(Frame frame, MethodRef methodRef)
        {
            var stack = frame.OperandStack;
            switch (methodRef.Descriptor)
            {
                case "(Z)V":    // boolean
                    // Java: true/false
                    // C#: True/False
                    Console.WriteLine(stack.Pop<int>() != 0 ? "true" : "false");
                    break;
                case "(C)V":    // char
                    Console.WriteLine(stack.Pop<int>());
                    break;
                case "(B)V":    // byte
                    Console.WriteLine(stack.Pop<int>());
                    break;
                case "(S)V":    // short
                    Console.WriteLine(stack.Pop<int>());
                    break;
                case "(I)V":    // int
                    Console.WriteLine(stack.Pop<int>());
                    break;
                case "(J)V":    // long
                    Console.WriteLine(stack.Pop<long>());
                    break;
                case "(F)V":    // float
                    Console.WriteLine(stack.Pop<float>());
                    break;
                case "(D)V":    // double
                    Console.WriteLine(stack.Pop<double>());
                    break;
                case "(Ljava/lang/String;)V":
                    var jString = stack.Pop<JavaObject>();
                    var charArr = (ArrayObject)jString.GetRefVar("value", "[C");
                    // Java和C#中字符串都是UTF-16编码，无需转换字符编码
                    var chars = charArr.Chars.Select(x => (char)x).ToArray();
                    Console.WriteLine(new string(chars));
                    break;
                default:
                    throw new Exception($"println: {methodRef.Descriptor}");
            }
            // fix: System.out从操作数栈中弹出
            stack.Pop<JavaObject>();
        }
    }
}
