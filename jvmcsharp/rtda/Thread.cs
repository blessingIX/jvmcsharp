namespace jvmcsharp.rtda
{
    internal class Thread
    {
        public int Pc { get; internal set; }
        public Stack Stack { get; internal set; } = new(1024);

        public void PushFrame(Frame frame) => Stack.Push(frame);

        public Frame PopFrame() => Stack.Pop();

        public Frame PeekFrame() => Stack.Peek();

        public Frame CraeteFrame(ushort maxLocals, ushort maxStack) => new(this, maxLocals, maxStack);
    }
}
