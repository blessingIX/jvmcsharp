using jvmcsharp.rtda.heap;

namespace jvmcsharp.rtda
{
    internal class Frame(Thread thread, Method method)
    {
        public Frame? Lower { get; internal set; }
        public LocalVars LocalVars { get; internal set; } = new LocalVars(method.MaxLocals);
        public OperandStack OperandStack { get; internal set; } = new OperandStack(method.MaxStack);
        public Thread Thread { get; internal set; } = thread;
        public Method Method { get; internal set; } = method;
        public int NextPc { get; internal set; }

        public void RevertNextPc() => NextPc = Thread.Pc;
    }
}
