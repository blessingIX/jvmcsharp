using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;

namespace jvmcsharp.instructions.references
{
    internal class INVOKE_INTERFACE : Instruction
    {
        public uint Index { get; internal set; }
        // public byte Count { get; internal set; }
        // public byte Zero {  get; internal set; }

        public override void FetchOperands(BytecodeReader reader)
        {
            Index = reader.ReadUInt16();
            reader.ReadUInt8(); // Count
            reader.ReadUInt8(); // must be 0
        }

        public override void Execute(Frame frame)
        {
            var cp = frame.Method.Class!.ConstantPool;
            var methodRef = cp.Get<InterfaceMethodRef>(Index);
            var resolvedMethod = methodRef.ResolveInterfaceMethod();
            if (resolvedMethod.IsStatic() || resolvedMethod.IsPrivate())
            {
                throw new Exception("java.lang.IncompatibleClassChangeError");
            }
            var @ref = frame.OperandStack.GetRefFromTop(resolvedMethod.ArgSlotCount - 1)
                ?? throw new Exception("java.lang.NullPointException");
            if (!@ref.Class.IsImplements(methodRef.ResolveClass()))
            {
                throw new Exception("java.lang.IncompatibleClassChageError");
            }
            var methodToBeInvoked = MethodRef.LookupMethodInClass(@ref.Class, methodRef.Name, methodRef.Descriptor);
            if (methodToBeInvoked == null || methodToBeInvoked.IsAbstract())
            {
                throw new Exception("java.lang.AbstractMethodError");
            }
            if (!methodToBeInvoked.IsPublic())
            {
                throw new Exception("java.lang.IllegalException");
            }
            CommonLogic.InvokeMethod(frame, methodToBeInvoked);
        }
    }
}
