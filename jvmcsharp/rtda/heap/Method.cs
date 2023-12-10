using jvmcsharp.classfile;
using static jvmcsharp.rtda.heap.AccessFlags;

namespace jvmcsharp.rtda.heap
{
    internal class Method : ClassMember
    {
        public ushort MaxStack { get; internal set; }
        public ushort MaxLocals { get; internal set; }
        public byte[] Code { get; internal set; } = [];
        public uint ArgSlotCount { get; internal set; }

        public Method(MemberInfo memberInfo) : base(memberInfo)
        {
            // copy attribute
            var codeAttr = memberInfo.CodeAttribute();
            if (codeAttr != null)
            {
                MaxStack = codeAttr.MaxStack;
                MaxLocals = codeAttr.MaxLocals;
                Code = codeAttr.Code;
            }
        }

        public static Method[] CreateMethods(Class @class, MemberInfo[] cfMethods)
            => cfMethods.Select(cfMethod =>
            {
                var method = new Method(cfMethod) { Class = @class };
                method.CalcArgSlotCount();
                return method;
            }).ToArray();

        public bool IsSynchronized() => 0 != (AccessFlags & ACC_SYNCHRONIZED);

        public bool IsBridge() => 0 != (AccessFlags & ACC_BRIDGE);

        public bool IsVarags() => 0 != (AccessFlags & ACC_VARARGS);

        public bool IsNative() => 0 != (AccessFlags & ACC_NATIVE);

        public bool IsAbstract() => 0 != (AccessFlags & ACC_ABSTRACT);

        public bool IsStrict() => 0 != (AccessFlags & ACC_STRICT);

        private void CalcArgSlotCount()
        {
            var parsedDescriptor = MethodDescriptorParser.ParseMethodDescriptor(Descriptor);
            foreach (var _ in parsedDescriptor.ParameterTypes)
            {
                ArgSlotCount++;
            }
            if (!IsStatic())
            {
                ArgSlotCount++;
            }
        }
    }
}
