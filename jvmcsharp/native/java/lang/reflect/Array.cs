using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;

namespace jvmcsharp.native.java.lang.reflect
{
    internal class Array
    {
        static Array()
        {
            Registry.Register("java/lang/reflect/Array", "newArray", "(Ljava/lang/Class;I)Ljava/lang/Object;", NewArray);
        }

        /// <summary>
        /// Creates a new array with the specified component type and length. Invoking this method is equivalent to creating an array as follows:
        /// int[] x = { length };
        /// Array.newInstance(componentType, x);
        /// </summary>
        private static void NewArray(Frame frame)
        {
            var componentType = (rtda.heap.Class)frame.LocalVars.Get<JavaObject>(0).Extra!;
            var length = frame.LocalVars.Get<int>(1);
            if (componentType == null)
            {
                throw new Exception("java.lang.NullPointerException");
            }
            var @void = rtda.heap.Class.PrimitiveTypes["void"];
            if (componentType.Name == @void || length > 255)
            {
                throw new Exception("java.lang.IllegalArgumentException");
            }
            if (length < 0)
            {
                throw new Exception("java.lang.NegativeArraySizeException");
            }
            var arrClass = componentType.ArrayClass();
            var arrObj = arrClass.NewArray((uint)length);
            frame.OperandStack.Push<JavaObject>(arrObj);
        }
    }
}
