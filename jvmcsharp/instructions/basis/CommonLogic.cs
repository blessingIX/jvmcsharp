using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;
using Thread = jvmcsharp.rtda.Thread;

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
                for (var i = argSlotCount - 1; i >= 0; i--)
                {
                    var slot = invokerFrame.OperandStack.Pop<object>();
                    newFrame.LocalVars.Set(i, slot);
                    if (i == 0) break;
                }
            }

            // hack
            if (method.IsNative())
            {
                if (method.Name == "registerNatives")
                {
                    thread.PopFrame();
                }
                else
                {
                    throw new Exception($"native method: {method.Class}.{method.Name}{method.Descriptor}");
                }
            }
        }

        public static void InitClass(Thread thread, Class @class)
        {
            @class.StartInit();
            ScheduleClinit(thread, @class);
            InitSuperClass(thread, @class);
        }

        private static void ScheduleClinit(Thread thread, Class @class)
        {
            var clinit = @class.GetClinitMethod();
            if (clinit != null)
            {
                // exec <clinit>
                var nweFrame = thread.CraeteFrame(clinit);
                thread.PushFrame(nweFrame);
            }
        }

        private static void InitSuperClass(Thread thread, Class @class)
        {
            if (!@class.IsInterface())
            {
                var superClass = @class.SuperClass;
                if (superClass != null && !superClass.InitStarted)
                {
                    InitClass(thread, superClass);
                }
            }
        }
    }
}
