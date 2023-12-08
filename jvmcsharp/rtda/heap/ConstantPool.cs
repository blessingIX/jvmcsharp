using jvmcsharp.classfile;

namespace jvmcsharp.rtda.heap
{
    internal class ConstantPool
    {
        public Class Class { get; internal set; }
        public object[] Consts { get; internal set; }

        public ConstantPool(Class @class, classfile.ConstantPool cfCp)
        {
            Class = @class;
            Consts = new object[cfCp.Length];
            for (uint i = 1; i < Consts.Length; i++)
            {
                var cfInfo = cfCp[i];
                if (cfInfo is ConstantIntegerInfo intInfo)
                {
                    this[i] = intInfo.Val;
                }
                else if (cfInfo is ConstantFloatInfo floatInfo)
                {
                    this[i] = floatInfo.Val;
                }
                else if (cfInfo is ConstantLongInfo longInfo)
                {
                    this[i] = longInfo.Val;
                }
                else if (cfInfo is ConstantDoubleInfo doubleInfo)
                {
                    this[i] = doubleInfo.Val;
                }
                else if (cfInfo is ConstantStringInfo stringInfo)
                {
                    this[i] = stringInfo.ToString();
                }
                else if (cfInfo is ConstantClassInfo classInfo)
                {
                    this[i] = new ClassRef(this, classInfo);
                }
                else if (cfInfo is ConstantFieldrefInfo fieldrefInfo)
                {
                    this[i] = new FieldRef(this, fieldrefInfo);
                }
                else if (cfInfo is ConstantMethodrefInfo methodrefInfo)
                {
                    this[i] = new MethodRef(this, methodrefInfo);
                }
                else if (cfInfo is ConstantInterfaceMethodrefInfo interfacemethodrefInfo)
                {
                    this[i] = new InterfaceMethodRef(this, interfacemethodrefInfo);
                }
            }
        }

        public object this[uint index]
        {
            get => Consts[index] ?? throw new Exception($"No constants at index {index}");
            private set => Consts[index] = value;
        }

        public T Get<T>(uint index) => (T)this[index];

        public void Set<T>(uint index, T value) => this[index] = value!;
    }
}
