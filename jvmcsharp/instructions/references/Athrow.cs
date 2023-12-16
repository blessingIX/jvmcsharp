using jvmcsharp.instructions.basis;
using jvmcsharp.native.java.lang;
using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;

namespace jvmcsharp.instructions.references
{
    internal class ATHROW : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var ex = frame.OperandStack.Pop<JavaObject>()
                ?? throw new Exception("java.lang.NullPointerException");
            var thread = frame.Thread;
            if (!FindAndGotoExceptionHandler(thread, ex))
            {
                HandleUncaughtException(thread, ex);
            }
        }

        private static bool FindAndGotoExceptionHandler(rtda.Thread thread, JavaObject ex)
        {
            while (true)
            {
                var frame = thread.PeekFrame();
                var pc = frame.NextPc - 1;
                var handlerPc = frame.Method.FindExceptionHandler(ex.Class, pc);
                if (handlerPc > 0)
                {
                    var stack = frame.OperandStack;
                    stack.Clear();
                    stack.Push(ex);
                    frame.NextPc = handlerPc;
                    return true;
                }
                thread.PopFrame();
                if (thread.IsStackEmpty())
                {
                    break;
                }
            }
            return false;
        }

        private static void HandleUncaughtException(rtda.Thread thread, JavaObject ex)
        {
            thread.ClearStack();
            var jMsg = ex.GetRefVar("detailMessage", "Ljava/lang/String;");
            var csMsg = StringPool.CsharpString(jMsg);
            Console.WriteLine($"{ex.Class.JavaName()}: {csMsg}");
            var stes = (StackTraceElement[])ex.Extra!;
            foreach (var ste in stes)
            {
                Console.WriteLine($"\tat {ste}");
            }
        }
    }
}
