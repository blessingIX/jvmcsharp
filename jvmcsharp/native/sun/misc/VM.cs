using jvmcsharp.instructions.basis;
using jvmcsharp.instructions.references;
using jvmcsharp.rtda;

namespace jvmcsharp.native.sun.misc
{
    internal class VM
    {
        static VM()
        {
            Registry.Register("sun/misc/VM", "initialize", "()V", Initialize);
        }

        private static void Initialize(Frame frame)
        {
            var vmClass = frame.Method.Class;
            var savedProps = vmClass!.GetRefVar("savedProps", "Ljava/util/Properties;");
            var key = StringPool.JavaString(vmClass.Loader!, "foo");
            var val = StringPool.JavaString(vmClass.Loader!, "bar");
            frame.OperandStack.Push(savedProps);
            frame.OperandStack.Push(key);
            frame.OperandStack.Push(val);
            var propsClass = vmClass.Loader!.LoadClass("java/util/Properties");
            var setPropMethod = propsClass.GetInstanceMethod("setProperty", "(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/Object;");
            CommonLogic.InvokeMethod(frame, setPropMethod);
        }
    }
}
