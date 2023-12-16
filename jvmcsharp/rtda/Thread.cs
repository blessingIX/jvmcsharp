using jvmcsharp.rtda.heap;

namespace jvmcsharp.rtda
{
    internal class Thread
    {
        public int Pc { get; internal set; }
        public Stack Stack { get; internal set; } = new(1024);

        public void PushFrame(Frame frame) => Stack.Push(frame);

        public Frame PopFrame() => Stack.Pop();

        public Frame PeekFrame() => Stack.Peek();

        public Frame CraeteFrame(Method method) => new(this, method);

        internal Frame TopFrame() => PeekFrame();

        internal bool IsStackEmpty() => Stack.IsEmpty();

        internal void ClearStack() => Stack.Clear();

        internal Frame[] GetFrames() => Stack.GetFrames();
    }
}
