using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;

namespace jvmcsharp.instructions.references
{
    internal class GET_STATIC : Index16Instruction
    {
        public override void Execute(Frame frame)
        {
            var currentMethod = frame.Method;
            var currentClass = currentMethod.Class!;
            var cp = currentClass.ConstantPool;
            var fieldRef = cp.Get<FieldRef>(Index);
            var field = fieldRef.ResolveField();
            var @class = field.Class!;

            if (!field.IsStatic())
            {
                throw new Exception("java.lang.IncompatibleClassChangeError");
            }

            var descriptor = field.Descriptor;
            var slotId = field.SlotId;
            var slots = @class.StaticVars;
            var stack = frame.OperandStack;
            switch (descriptor[0])
            {
                case 'Z':
                case 'B':
                case 'C':
                case 'S':
                case 'I':
                    stack.Push(slots.Get<int>(slotId));
                    break;
                case 'F':
                    stack.Push(slots.Get<float>(slotId));
                    break;
                case 'J':
                    stack.Push(slots.Get<long>(slotId));
                    break;
                case 'D':
                    stack.Push(slots.Get<double>(slotId));
                    break;
                case 'L':
                case '[':
                    stack.Push(slots.Get<JavaObject>(slotId));
                    break;
            }
        }
    }
}
