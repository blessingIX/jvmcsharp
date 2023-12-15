using jvmcsharp.rtda;

namespace jvmcsharp.native.java.lang
{
    internal class Object
    {
        static Object()
        {
            Registry.Register("java/lang/Object", "getClass", "()Ljava/lang/Class;", GetClass);
            Registry.Register("java/lang/Object", "hashCode", "()I", HashCode);
        }

        private static void GetClass(Frame frame)
        {
            var @this = frame.LocalVars.GetThis();
            var @class = @this.Class.JClass;
            frame.OperandStack.Push(@class);
        }

        private static void HashCode(Frame frame)
        {
            var @this = frame.LocalVars.GetThis();
            var hash = @this.GetHashCode();
            frame.OperandStack.Push(hash);
        }
    }
}
