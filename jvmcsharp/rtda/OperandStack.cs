using jvmcsharp.rtda.heap;

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

        public JavaObject GetRefFromTop(uint n) => (JavaObject)Slot[Size - 1 - n];

        public void Clear()
        {
            Size = 0;
            for (int i = 0; i < Slot.Length; i++)
            {
                Slot[i] = null!;
            }
        }
    }
}
