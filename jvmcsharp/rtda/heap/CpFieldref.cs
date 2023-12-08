using jvmcsharp.classfile;

namespace jvmcsharp.rtda.heap
{
    internal class FieldRef : MemberRef
    {
        public Field? Field { get; internal set; }

        public FieldRef(ConstantPool cp, ConstantFieldrefInfo refInfo) : base(refInfo)
        {
            Cp = cp;
        }

        public Field ResolveField()
        {
            if (Field == null)
            {
                ResolveFiledRef();
            }
            return Field!;
        }

        private void ResolveFiledRef()
        {
            var d = Cp.Class;
            var c = ResolveClass();
            var field = LookupField(c, Name, Descriptor) ?? throw new Exception("java.lang.NoSuchFieldError");
            if (!field.IsAccessibleTo(d))
            {
                throw new Exception("java.lang.IllegalAccessError");
            }
            Field = field;
        }

        private Field LookupField(Class c, string name, string descriptor)
        {
            foreach (var field in c.Fields)
            {
                if (field.Name == name && field.Descriptor == descriptor)
                {
                    return field;
                }
            }

            foreach (var iface in c.Interfaces)
            {
                var field = LookupField(iface, name, descriptor);
                if (field != null) return field;
            }

            if (c.SuperClass != null)
            {
                return LookupField(c.SuperClass, name, descriptor);
            }

            return null!;
        }
    }
}
