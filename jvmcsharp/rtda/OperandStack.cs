namespace jvmcsharp.rtda
{
    internal class OperandStack(uint maxStack)
    {
        public uint Size { get; internal set; }
        public object[] Slot { get; internal set; } = new object[maxStack];

        public void Push<T>(T val)
        {
            Slot[Size] = val!;
            Size++;
        }

        public T Pop<T>()
        {
            Size--;
            var val = (T)Slot[Size];
            if (Slot[Size] is not ValueType)
                Slot[Size] = null!;
            return val!;
        }
    }
}
