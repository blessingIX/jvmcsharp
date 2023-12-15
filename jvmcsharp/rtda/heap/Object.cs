





namespace jvmcsharp.rtda.heap
{
    internal class JavaObject
    {
        public Class Class { get; internal set; }
        public object Data { get; internal set; }
        public object? Extra { get; internal set; }
        public LocalVars Fields => (LocalVars)Data;

        internal JavaObject()
        {
            Class = null!;
            Data = null!;
        }

        public JavaObject(Class @class)
        {
            Class = @class;
            Data = new LocalVars(@class.InstanceSlotCount);
        }

        public bool IsInstanceOf(Class @class) => @class.IsAssignableFrom(Class);

        public void SetRefVar(string name, string descriptor, JavaObject @ref)
        {
            var field = Class.GetField(name, descriptor, false);
            Fields.Set(field.SlotId, @ref);
        }

        public JavaObject GetRefVar(string name, string descriptor)
        {
            var field = Class.GetField(name, descriptor, false);
            return Fields.Get<JavaObject>(field.SlotId);
        }

        public JavaObject Clone() => new JavaObject(Class) { Data = CloneData() };

        private object CloneData()
        {
            if (Data is sbyte[] sbyteArr)
            {
                return GenericCloneData<sbyte>(sbyteArr);
            }
            else if (Data is short[] shortArr)
            {
                return GenericCloneData<short>(shortArr);
            }
            else if (Data is ushort[]  ushortArr)
            {
                return GenericCloneData<ushort>(ushortArr);
            }
            else if (Data is int[] intArr)
            {
                return GenericCloneData<int>(intArr);
            }
            else if (Data is long[] longArr)
            {
                return GenericCloneData<long>(longArr);
            }
            else if (Data is float[] floatArr)
            {
                return GenericCloneData<float>(floatArr);
            }
            else if (Data is double[] doubleArr)
            {
                return GenericCloneData<double>(doubleArr);
            }
            else if (Data is JavaObject[] javaObjectArr)
            {
                return GenericCloneData<JavaObject>(javaObjectArr);
            }
            else
            {
                var localVars = (LocalVars)Data;
                var slots = new LocalVars((uint)localVars.Slots.Length);
                Array.Copy(localVars.Slots, slots.Slots, localVars.Slots.Length);
                return slots;
            }
        }

        private static object GenericCloneData<T>(Array arr)
        {
            var newArr = new T[arr.Length];
            Array.Copy(arr, newArr, arr.Length);
            return newArr;
        }
    }
}
