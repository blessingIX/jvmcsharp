namespace jvmcsharp.rtda
{
    internal class Stack(uint maxSize)
    {
        public uint MaxSize { get; private set; } = maxSize;
        public uint Size { get; private set; }
        public Frame? Top { get; private set; }

        public void Push(Frame frame)
        {
            if (Size >= MaxSize)
            {
                throw new Exception("java.lang.StackOverflowError");
            }
            if (Top != null)
            {
                frame.Lower = Top;
            }
            Top = frame;
            Size++;
        }

        public Frame Pop()
        {
            if (Top == null)
            {
                throw new Exception("jvm stack is empty!");
            }
            var top = Top;
            Top = top.Lower;
            top.Lower = null!;
            Size--;
            return top;
        }

        public Frame Peek()
        {
            if (Top == null)
            {
                throw new Exception("jvm stack is empty!");
            }
            return Top;
        }

        public bool IsEmpty() => Top == null;
    }
}
