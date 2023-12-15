using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;

namespace jvmcsharp.native.java.lang
{
    internal class System
    {
        static System()
        {
            Registry.Register("java/lang/System", "arraycopy", "(Ljava/lang/Object;ILjava/lang/Object;II)V", Arraycopy);
        }

        private static void Arraycopy(Frame frame)
        {
            var vars = frame.LocalVars;
            var src = vars.Get<ArrayObject>(0);
            var srcPos = vars.Get<int>(1);
            var dest = vars.Get<ArrayObject>(2);
            var destPos = vars.Get<int>(3);
            var length = vars.Get<int>(4);

            if (src == null || dest == null)
            {
                throw new Exception("java.lang.NullPointerException");
            }
            if (!CheckArrayCopy(src, dest))
            {
                throw new Exception("java.lang.ArrayStoreException");
            }
            if (srcPos < 0 || destPos < 0 || length < 0
                || srcPos + length > src.ArrayLength()
                || destPos + length > dest.ArrayLength())
            {
                throw new Exception("java.lang.IndexOutOfBoundsException");
            }
            Array.Copy((Array)src.Data, srcPos, (Array)dest.Data, destPos, length);
        }

        private static bool CheckArrayCopy(ArrayObject src, ArrayObject dest)
        {
            var srcClass = src.Class;
            var destClass = dest.Class;
            if (!srcClass.IsArray()
                || !destClass.IsArray())
            {
                return false;
            }

            if (srcClass.ComponentClass().IsPrimitive()
                || destClass.ComponentClass().IsPrimitive())
            {
                return srcClass == destClass;
            }
            return true;
        }
    }
}
