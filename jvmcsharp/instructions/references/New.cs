using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;

namespace jvmcsharp.instructions.references
{
    internal class NEW : Index16Instruction
    {
        public override void Execute(Frame frame)
        {
            var cp = frame.Method.Class!.ConstantPool;
            var classRef = cp.Get<ClassRef>(Index);
            var @class = classRef.ResolveClass();
            if (@class.IsInterface() || @class.IsAbstract())
            {
                throw new Exception("InstantiationError");
            }
            var @ref = @class.NewObject();
            frame.OperandStack.Push(@ref);
        }
    }
}
