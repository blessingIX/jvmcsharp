using jvmcsharp.rtda;

namespace jvmcsharp.native.java.lang
{
    internal class Float
    {
        static Float()
        {
            Registry.Register("java/lang/Float", "floatToRawIntBits", "(F)I", FloatToRawIntBits);
        }

        private static void FloatToRawIntBits(Frame frame)
        {
            var value = frame.LocalVars.Get<float>(0);
            frame.OperandStack.Push(BitConverter.ToInt32(BitConverter.GetBytes(value), 0));
        }
    }
}
