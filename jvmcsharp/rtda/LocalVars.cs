
using jvmcsharp.rtda.heap;

namespace jvmcsharp.rtda
{
    internal class LocalVars(uint maxLocals)
    {
        public object[] Slots { get; internal set; } = new object[maxLocals];

        public T Get<T>(uint index)
        {
            if (typeof(T).IsValueType && Slots[index] == null)
            {
                // LocalVars作为类的字段的容器时，没有设置初始值的数值类型字段会使用数据类型的默认值
                // int long float double类型的默认值都是0
                // 获取没有设置初始值的数值类型字段值时先设置默认值0
                Slots[index] = 0;
            }
            return (T)Slots[index];
        }

        public void Set<T>(uint index, T value) => Slots[index] = value!;

        internal JavaObject GetThis() => Get<JavaObject>(0);
    }
}
