
using jvmcsharp.rtda.heap;

namespace jvmcsharp.rtda
{
    internal class LocalVars(uint maxLocals)
    {
        public object[] Slots { get; internal set; } = new object[maxLocals];

        public T Get<T>(uint index) => (T)Slots[index];

        public void Set<T>(uint index, T value) => Slots[index] = value!;

        internal JavaObject GetThis() => Get<JavaObject>(0);
    }
}
