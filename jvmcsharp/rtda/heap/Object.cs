
namespace jvmcsharp.rtda.heap
{
    internal class JavaObject
    {
        public Class Class { get; internal set; }
        public object Data { get; internal set; }
        public LocalVars Fields => (LocalVars)Data;

        protected JavaObject()
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
    }
}
