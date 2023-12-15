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
        public bool InitStarted { get; internal set; }
        public JavaObject JClass { get; internal set; } // java.lang.Class实例

        internal static readonly Dictionary<string, string> PrimitiveTypes = new()
        {
            { "void", "V" },
            { "boolean", "Z" },
            { "byte", "B" },
            { "short", "S" },
            { "int", "I" },
            { "long", "J" },
            { "char", "C" },
            { "float", "F" },
            { "double", "D" },
        };

#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
        internal Class() {}
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
        public Class(ClassFile cf)
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
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
            if (!other.IsArray())
            {
                if (!other.IsInterface())
                {
                    if (IsInterface())
                    {
                        return other.IsSubClassOf(this);
                    }
                    else
                    {
                        return other.IsImplements(this);
                    }
                }
                else
                {
                    if (!IsInterface())
                    {
                        return IsJlObject();
                    }
                    else
                    {
                        return IsSuperInterfaceOf(other);
                    }
                }
            }
            else
            {
                if (IsArray())
                {
                    if (!IsInterface())
                    {
                        return IsJlObject();
                    }
                    else
                    {
                        return IsJlCloneable() || IsJioSerializable();
                    }
                }
                else
                {
                    var sc = other.ComponentClass();
                    var tc = ComponentClass();
                    return sc == tc || tc.IsAssignableFrom(sc);
                }
            }
        }

        private bool IsSuperInterfaceOf(Class iface)
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

        private bool IsJlObject() => Name == "java/lang/Object";

        private bool IsJlCloneable() => Name == "java/lang/Cloneable";

        private bool IsJioSerializable() => Name == "java/io/Serializable";

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

        public Method GetMainMethod()
        {
            var method = GetStaticMethod("main", "([Ljava/lang/String;)V");
            if (method != null && (!method.IsPublic() || !method.IsStatic())) method = null;
            return method!;
        }

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

        public void StartInit() => InitStarted = true;

        public Method GetClinitMethod() => GetStaticMethod("<clinit>", "()V");

        public ArrayObject NewArray(uint count)
        {
            if (!IsArray())
            {
                throw new Exception($"Not array class: {Name}");
            }
            return Name switch
            {
                "[Z" => new ArrayObject { Class = this, Data = new sbyte[count] },
                "[B" => new ArrayObject { Class = this, Data = new sbyte[count] },
                "[C" => new ArrayObject { Class = this, Data = new short[count] },
                "[S" => new ArrayObject { Class = this, Data = new short[count] },
                "[I" => new ArrayObject { Class = this, Data = new int[count] },
                "[J" => new ArrayObject { Class = this, Data = new long[count] },
                "[F" => new ArrayObject { Class = this, Data = new float[count] },
                "[D" => new ArrayObject { Class = this, Data = new double[count] },
                _ => new ArrayObject { Class = this, Data = new JavaObject[count] },
            };
        }

        public bool IsArray() => Name[0] == '[';

        public Class ArrayClass()
        {
            var arrayClassName = GetArrayClassName(Name);
            return Loader!.LoadClass(arrayClassName);
        }

        public static string GetArrayClassName(string name) => $"[{ToDescritptor(name)}";

        private static string ToDescritptor(string name)
        {
            if (name[0] == '[')
            {
                return name;
            }
            if (PrimitiveTypes.TryGetValue(name, out var type))
            {
                return type;
            }
            return $"L{name}";
        }

        public Class ComponentClass() => Loader!.LoadClass(GetComponentClassName(Name));

        private static string GetComponentClassName(string name)
        {
            if (name[0] == '[')
            {
                var componentTypeDescriptor = name[1..^0];
                return ToClassName(componentTypeDescriptor);
            }
            throw new Exception($"Not array: {name}");
        }

        private static string ToClassName(string descriptor)
        {
            if (descriptor[0] == '[')   // array
            {
                return descriptor;
            }
            if (descriptor[0] == 'L')   // object
            {
                return descriptor[1..^1];
            }
            foreach (var (className, d) in PrimitiveTypes)
            {
                if (d == descriptor)    // primitive
                {
                    return className;
                }
            }
            throw new Exception($"Invalid descriptor: {descriptor}");
        }

        public Field GetField(string name, string descriptor, bool isStatic)
        {
            for (var c = this; c != null; c = c.SuperClass)
            {
                foreach (var field in c.Fields)
                {
                    if (field.IsStatic() == isStatic
                        && field.Name == name
                        && field.Descriptor == descriptor)
                    {
                        return field;
                    }
                }
            }
            return null!;
        }

        public string JavaName() => Name.Replace('/', '.');

        public bool IsPrimitive() => PrimitiveTypes.ContainsKey(Name);
    }
}
