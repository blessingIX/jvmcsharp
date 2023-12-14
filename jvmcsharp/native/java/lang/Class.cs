using jvmcsharp.instructions.references;
using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;

namespace jvmcsharp.native.java.lang
{
    internal class Class
    {
        static Class()
        {
            Registry.Register("java/lang/Class", "getPrimitiveClass", "(Ljava/lang/String;)Ljava/lang/Class;", GetPrimitiveClass);
            Registry.Register("java/lang/Class", "getName0", "()Ljava/lang/String;", GetName0);
            Registry.Register("java/lang/Class", "desiredAssertionStatus0", "(Ljava/lang/Class;)Z", DesiredAssertionStatus0);
        }

        private static void GetPrimitiveClass(Frame frame)
        {
            var nameObj = frame.LocalVars.Get<JavaObject>(0);
            var name = StringPool.CsharpString(nameObj);
            var loader = frame.Method.Class!.Loader!;
            var @class = loader.LoadClass(name).JClass;
            frame.OperandStack.Push(@class);
        }

        private static void GetName0(Frame frame)
        {
            var @this = frame.LocalVars.GetThis();
            var @class = (rtda.heap.Class)@this.Extra!;
            var name = @class.JavaName();
            var nameObj = StringPool.JavaString(@class.Loader!, name);
            frame.OperandStack.Push(nameObj);
        }

        private static void DesiredAssertionStatus0(Frame frame)
        {
            // TODO 暂不讨论断言
            frame.OperandStack.Push(0);
        }
    }
}
