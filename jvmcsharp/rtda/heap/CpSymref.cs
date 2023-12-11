
namespace jvmcsharp.rtda.heap
{
    internal class SymRef
    {
        public ConstantPool? Cp { get; internal set; }
        public string ClassName { get; internal set; } = string.Empty;
        public Class? Class { get; internal set; }

        public Class ResolveClass()
        {
            if (Class == null)
            {
                ResolveClassRef();
            }
            return Class!;
        }

        private void ResolveClassRef()
        {
            var d = Cp!.Class;
            var c = d.Loader!.LoadClass(ClassName);
            if (!c.IsAccessibleTo(d))
            {
                throw new Exception("java.lang.IllegalAccessError");
            }
            Class = c;
        }
    }
}
