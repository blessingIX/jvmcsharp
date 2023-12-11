



namespace jvmcsharp.rtda.heap
{
    internal class JavaObject
    {
        public Class Class { get; internal set; }
        public object Data { get; internal set; }
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
    }
}
