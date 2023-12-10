using jvmcsharp.classfile;

namespace jvmcsharp.rtda.heap
{
    internal class InterfaceMethodRef : MemberRef
    {
        public Method? Method { get; internal set; }

        public InterfaceMethodRef(ConstantPool cp, ConstantInterfaceMethodrefInfo refInfo) : base(refInfo)
            => Cp = cp;

        public Method ResolveInterfaceMethod()
        {
            if (Method == null)
            {
                ResolveInterfaceMethodRef();
            }
            return Method!;
        }

        private void ResolveInterfaceMethodRef()
        {
            var d = Cp.Class;
            var c = ResolveClass();
            if (!c.IsInterface())
            {
                throw new Exception("java.lang.IncompatibleClassChangeError");
            }
            var method = LookupInterfaceMethod(c, Name, Descriptor)
                ?? throw new Exception("java.lang.NoSuchMethodError");
            if (!method.IsAccessibleTo(d))
            {
                throw new Exception("java.lang.IllegalAccessError");
            }
            Method = method;
        }

        private static Method LookupInterfaceMethod(Class iface, string name, string descriptor)
        {
            foreach (var method in iface.Methods)
            {
                if (method.Name == name && method.Descriptor == descriptor)
                {
                    return method;
                }
            }
            return MethodRef.LookupMethodInInterfaces(iface.Interfaces, name, descriptor);
        }
    }
}
