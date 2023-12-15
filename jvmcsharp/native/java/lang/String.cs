using jvmcsharp.instructions.references;
using jvmcsharp.rtda;

namespace jvmcsharp.native.java.lang
{
    internal class String
    {
        static String()
        {
            Registry.Register("java/lang/String", "intern", "()Ljava/lang/String;", Intern);
        }

        private static void Intern(Frame frame)
        {
            var @this = frame.LocalVars.GetThis();
            var interned = StringPool.InternedString(@this);
            frame.OperandStack.Push(interned);
        }
    }
}
