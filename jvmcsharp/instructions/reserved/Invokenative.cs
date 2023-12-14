using jvmcsharp.instructions.basis;
using jvmcsharp.native;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.reserved
{
    internal class INVOKE_NATIVE : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var method = frame.Method;
            var className = method.Class!.Name;
            var methodName = method.Name;
            var methodDescriptor = method.Descriptor;
            var nativeMethod = Registry.FindNativeMethod(className, methodName, methodDescriptor)
                ?? throw new Exception($"java.lang.UnsatisfiedLinkError: {className}.{methodName}{methodDescriptor}");
            // invoke native method
            nativeMethod(frame);
        }
    }
}
