using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;

namespace jvmcsharp.instructions.basis
{
    internal static class CommonLogic
    {
        public static void Branch(Frame frame, int offset)
        {
            var pc = frame.Thread.Pc;
            var nextPc = pc + offset;
            frame.NextPc = nextPc;
        }

        public static void InvokeMethod(Frame invokerFrame, Method method)
        {
            var thread = invokerFrame.Thread;
            var newFrame = thread.CraeteFrame(method);
            thread.PushFrame(newFrame);
            var argSlotCount = method.ArgSlotCount;
            if (argSlotCount > 0)
            {
                for ( var i = argSlotCount - 1; i >= 0; i--)
                {
                    var slot = invokerFrame.OperandStack.Pop<object>();
                    newFrame.LocalVars.Set(i, slot);
                    if (i == 0) break;
                }
            }
        }
    }
}
