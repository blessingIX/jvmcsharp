using jvmcsharp.classfile;
using static jvmcsharp.rtda.heap.AccessFlags;

namespace jvmcsharp.rtda.heap
{
    internal class Field : ClassMember
    {
        public uint ConstValueIndex { get; internal set; }
        public uint SlotId { get; internal set; }

        public Field(Class @class, MemberInfo memberInfo) : base(@class, memberInfo)
        {
            var valAttr = memberInfo.ConstantValueAttribute();
            if (valAttr != null)
            {
                ConstValueIndex = valAttr.ConstantValueIndex;
            }
        }

        public static Field[] CreateFields(Class @class, MemberInfo[] cfFields)
            => cfFields.Select(cfFiled => new Field(@class, cfFiled)).ToArray();

        public bool IsVolatile() => 0 != (AccessFlags & ACC_VOLATILE);

        public bool IsTransient() => 0 != (AccessFlags & ACC_TRANSIENT);

        public bool IsEnum() => 0 != (AccessFlags & ACC_ENUM);

        public bool IsLongOrDouble() => Descriptor == "J" || Descriptor == "D";
    }
}
