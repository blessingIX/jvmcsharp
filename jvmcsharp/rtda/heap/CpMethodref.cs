using jvmcsharp.classfile;

namespace jvmcsharp.rtda.heap
{
    internal class MethodRef : MemberRef
    {
        public Method? Method { get; internal set; }

        public MethodRef(ConstantPool cp, ConstantMethodrefInfo refInfo) : base(refInfo) => Cp = cp;

        public Method ResolveMethod()
        {
            if (Method == null)
            {
                ResolveMethodRef();
            }
            return Method!;
        }

        private void ResolveMethodRef()
        {
            var d = Cp!.Class!;
            var c = ResolveClass();
            if (c.IsInterface())
            {
                throw new Exception("java.lang.IncompatibleClassChangeError");
            }
            var method = LookupMethod(c, Name, Descriptor) ?? throw new Exception("java.lang.NoSuchMethodError");
            if (!method.IsAccessibleTo(d))
            {
                throw new Exception("java.lang.IllegalAccessError");
            }
            Method = method;
        }

        private Method LookupMethod(Class c, string name, string descriptor)
        {
            var method = LookupMethodInClass(c, name, descriptor);
            method ??= LookupMethodInInterfaces(c.Interfaces, name, descriptor);
            return method!;
        }

        public static Method LookupMethodInClass(Class @class, string name, string descriptor)
        {
            var c = @class;
            while (c != null)
            {
                foreach (var method in c.Methods)
                {
                    if (method.Name == name && method.Descriptor == descriptor)
                        return method;
                }
                c = c.SuperClass;
            }
            return null!;
        }

        public static Method LookupMethodInInterfaces(Class[] interfaces, string name, string descriptor)
        {
            foreach (var iface in interfaces)
            {
                foreach (var method in iface.Methods)
                {
                    if (method.Name == name && method.Descriptor == descriptor)
                    {
                        return method;
                    }
                }
                var m = LookupMethodInInterfaces(iface.Interfaces, name, descriptor);
                if (m != null) return m;
            }
            return null!;
        }
    }
}
