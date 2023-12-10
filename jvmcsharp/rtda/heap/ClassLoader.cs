using jvmcsharp.classfile;
using jvmcsharp.classpath;

namespace jvmcsharp.rtda.heap
{
    internal class ClassLoader(Classpath classpath)
    {
        public Classpath Cp { get; internal set; } = classpath;
        public Dictionary<string, Class> ClassMap { get; internal set; } = [];
        public bool VerboseFlag { get; internal set; }

        public Class LoadClass(string name)
            => ClassMap.TryGetValue(name, out Class? value) ? value : LoadNonArrayClass(name);

        private Class LoadNonArrayClass(string name)
        {
            if (VerboseFlag)
            {
                Console.WriteLine($"[Ready to load {name}]");
            }
            var (data, entry) = ReadClass(name);
            var @class = DefineClass(data);
            Link(@class);
            if (VerboseFlag)
            {
                Console.WriteLine($"[Loaded {name} from {entry}]");
            }
            return @class;
        }

        private void Link(Class @class)
        {
            Verify(@class);
            Prepare(@class);
        }

        private void Verify(Class @class)
        {
            // TODO
        }

        private void Prepare(Class @class)
        {
            CalcInstanceFieldSlotIds(@class);
            CalcStaticFieldSlotIds(@class);
            AllocAndInitStaticVars(@class);
        }

        private void CalcInstanceFieldSlotIds(Class @class)
        {
            uint slotId = @class.SuperClass != null ? @class.SuperClass.InstanceSlotCount : 0;
            foreach (var field in @class.Fields)
            {
                if (!field.IsStatic())
                {
                    field.SlotId = slotId;
                    slotId++;
                }
            }
            @class.InstanceSlotCount = slotId;
        }

        private void CalcStaticFieldSlotIds(Class @class)
        {
            uint slotId = 0;
            foreach (var field in @class.Fields)
            {
                if (field.IsStatic())
                {
                    field.SlotId = slotId;
                    slotId++;
                }
            }
            @class.StaticSlotCount = slotId;
        }

        private void AllocAndInitStaticVars(Class @class)
        {
            @class.StaticVars = new(@class.StaticSlotCount);
            foreach (var field in @class.Fields)
            {
                // TODO 这里只对静态且final的字段进行初始化，非final的静态字段却没初始化
                // 猜测：这里是使用Field的ConstValueIndex（常量值索引）去找常量为字段赋值，只有静态且final的字段才是常量，在常量池中才有记录
                if (field.IsStatic() && field.IsFinal())
                {
                    InitStaticFinalVar(@class, field);
                }
            }
        }

        private void InitStaticFinalVar(Class @class, Field field)
        {
            var vars = @class.StaticVars;
            var cp = @class.ConstantPool;
            var cpIndex = field.ConstValueIndex;
            var slotId = field.SlotId;
            if (cpIndex > 0)
            {
                switch (field.Descriptor)
                {
                    case "Z":
                    case "B":
                    case "C":
                    case "S":
                    case "I":
                        vars.Set(slotId, cp.Get<int>(cpIndex));
                        break;
                    case "J":
                        vars.Set(slotId, cp.Get<long>(cpIndex));
                        break;
                    case "F":
                        vars.Set(slotId, cp.Get<float>(cpIndex));
                        break;
                    case "D":
                        vars.Set(slotId, cp.Get<double>(cpIndex));
                        break;
                    case "Ljava/lang/String":
                        throw new Exception("TODO Ljava/lang/String");
                }
            }
        }

        private Class DefineClass(byte[] data)
        {
            var @class = ParseClass(data);
            @class.Loader = this;
            ResolveSuperClass(@class);
            ResolveInterface(@class);
            ClassMap[@class.Name] = @class;
            return @class;
        }

        private void ResolveInterface(Class @class)
            => @class.Interfaces = @class.InterfaceNames.Select(@class.Loader!.LoadClass).ToArray();

        private void ResolveSuperClass(Class @class)
        {
            if (@class.Name != "java/lang/Object")
            {
                @class.SuperClass = @class.Loader!.LoadClass(@class.SuperClassName);
            }
        }

        private Class ParseClass(byte[] data)
        {
            try
            {
                return new Class(new ClassFile(data));
            }
            catch (Exception)
            {
                throw new Exception("java.lang.ClassFormatError");
            }
        }

        private (byte[], IEntry) ReadClass(string name)
        {
            try
            {
                return Cp.ReadClass(name);
            }
            catch (Exception)
            {
                throw new Exception($"java.lang.ClassNotFoundException: {name}");
            }
        }
    }
}
