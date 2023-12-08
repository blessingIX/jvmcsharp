using jvmcsharp.classfile;
using static jvmcsharp.rtda.heap.AccessFlags;

namespace jvmcsharp.rtda.heap
{
    internal class ClassMember(MemberInfo memberInfo)
    {
        public ushort AccessFlags { get; internal set; } = memberInfo.AccessFlags;
        public string Name { get; internal set; } = memberInfo.Name();
        public string Descriptor { get; internal set; } = memberInfo.Descriptor();
        public Class? Class { get; internal set; }

        public bool IsPublic() => 0 != (AccessFlags & ACC_PUBLIC);

        public bool IsPrivate() => 0 != (AccessFlags & ACC_PRIVATE);

        public bool IsProtected() => 0 != (AccessFlags & ACC_PROTECTED);

        public bool IsStatic() => 0 != (AccessFlags & ACC_STATIC);

        public bool IsFinal() => 0 != (AccessFlags & ACC_FINAL);

        public bool IsSynthetic() => 0 != (AccessFlags & ACC_SYNTHETIC);

        public bool IsAccessibleTo(Class d)
        {
            // public
            if (IsPublic())
            {
                return true;
            }

            var c = Class!;
            // protected
            if (IsProtected())
            {
                return d == c || d.IsSubClassOf(c) || c.GetPackageName() == d.GetPackageName();
            }

            // 默认访问控制符（同package内可访问）
            if (!IsPrivate())
            {
                return c.GetPackageName() == d.GetPackageName();
            }

            // private
            return d == c;
        }
    }
}
