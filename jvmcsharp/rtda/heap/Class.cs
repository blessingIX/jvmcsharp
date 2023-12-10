using jvmcsharp.classfile;
using static jvmcsharp.rtda.heap.AccessFlags;

namespace jvmcsharp.rtda.heap
{
    internal class Class
    {
        public ushort AccessFlags { get; internal set; }
        public string Name { get; internal set; } = string.Empty;
        public string SuperClassName { get; internal set; } = string.Empty;
        public string[] InterfaceNames { get; internal set; }
        public ConstantPool ConstantPool { get; internal set; }
        public Field[] Fields { get; internal set; } = [];
        public Method[] Methods { get; internal set; } = [];
        public ClassLoader? Loader { get; internal set; }
        public Class? SuperClass { get; internal set; }
        public Class[] Interfaces { get; internal set; } = [];
        public uint InstanceSlotCount { get; internal set; }
        public uint StaticSlotCount { get; internal set; }
        public LocalVars StaticVars { get; internal set; } = new(0);

        public Class(ClassFile cf)
        {
            AccessFlags = cf.AccessFlags;
            Name = cf.ClassName();
            SuperClassName = cf.SuperClassName();
            InterfaceNames = cf.InterfaceNames();
            ConstantPool = new ConstantPool(this, cf.ConstantPool);
            Fields = Field.CreateFields(this, cf.Fileds);
            Methods = Method.CreateMethods(this, cf.Methods);
        }

        public bool IsPublic() => 0 != (AccessFlags & ACC_PUBLIC);

        public bool IsFinal() => 0 != (AccessFlags & ACC_FINAL);

        public bool IsSuper() => 0 != (AccessFlags & ACC_SUPER);

        public bool IsInterface() => 0 != (AccessFlags & ACC_INTERFACE);

        public bool IsAbstract() => 0 != (AccessFlags & ACC_ABSTRACT);

        public bool IsSynthetic() => 0 != (AccessFlags & ACC_SYNTHETIC);

        public bool IsAnnotation() => 0 != (AccessFlags & ACC_ANNOTATION);

        public bool IsEnum() => 0 != (AccessFlags & ACC_ENUM);

        public bool IsAccessibleTo(Class other) => IsPublic() || GetPackageName() == other.GetPackageName();

        public string GetPackageName()
        {
            var i = Name.LastIndexOf('/');
            return i >= 0 ? Name[0..i] : string.Empty;
        }

        public JavaObject NewObject() => new(this);

        public bool IsAssignableFrom(Class other)
        {
            if (other == this)
            {
                return true;
            }
            if (!IsInterface())
            {
                return other.IsSubClassOf(this);
            } else
            {
                return other.IsImplements(this);
            }
        }

        public bool IsSubClassOf(Class other)
        {
            var c = SuperClass;
            while (c != null)
            {
                if (c == other) return true;
                c = c.SuperClass;
            }
            return false;
        }

        public bool IsImplements(Class iface)
        {
            var c = this;
            while (c != null)
            {
                foreach (var i in c.Interfaces)
                {
                    if (i == iface || i.IsSubInterfaceOf(iface))
                    {
                        return true;
                    }
                }
                c = c.SuperClass;
            }
            return false;
        }

        public bool IsSubInterfaceOf(Class iface)
        {
            foreach (var superInterface in Interfaces)
            {
                if (superInterface == iface || superInterface.IsSubInterfaceOf(iface))
                {
                    return true;
                }
            }
            return false;
        }

        public Method GetMainMethod() => GetStaticMethod("main", "([Ljava/lang/String;)V");

        public Method GetStaticMethod(string name, string descriptor)
        {
            foreach (var method in Methods)
            {
                if (method.Name == name && method.Descriptor == descriptor)
                {
                    return method;
                }
            }
            return null!;
        }

        public bool IsSuperClassOf(Class other) => other.IsSubClassOf(this);
    }
}
