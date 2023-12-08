
namespace jvmcsharp.rtda.heap
{
    internal class JavaObject(Class @class)
    {
        public Class Class { get; internal set; } = @class;
        public LocalVars Fields { get; internal set; } = new LocalVars(@class.InstanceSlotCount);

        internal bool IsInstanceOf(Class @class) => @class.IsAssignableFrom(Class);
    }
}
