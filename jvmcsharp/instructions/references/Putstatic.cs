using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;

namespace jvmcsharp.instructions.references
{
    internal class PUT_STATIC : Index16Instruction
    {
        public override void Execute(Frame frame)
        {
            var currentMethod = frame.Method;
            var currentClass = currentMethod.Class!;
            var cp = currentClass.ConstantPool;
            var fieldRef = cp.Get<FieldRef>(Index);
            var field = fieldRef.ResolveField();
            var @class = field.Class!;

            if (!@class.InitStarted)
            {
                frame.RevertNextPc();
                CommonLogic.InitClass(frame.Thread, @class);
                return;
            }

            if (!field.IsStatic())
            {
                throw new Exception("java.lang.IncompatibleClassChangeError");
            }
            if (field.IsFinal())
            {
                if (currentClass != @class || currentMethod.Name != "<clinit>")
                {
                    throw new Exception("java.lang.IllegalAccessError");
                }
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
                    slots.Set(slotId, stack.Pop<int>());
                    break;
                case 'F':
                    slots.Set(slotId, stack.Pop<float>());
                    break;
                case 'J':
                    slots.Set(slotId, stack.Pop<long>());
                    break;
                case 'D':
                    slots.Set(slotId, stack.Pop<double>());
                    break;
                case 'L':
                case '[':
                    slots.Set(slotId, stack.Pop<JavaObject>());
                    break;
            }
        }
    }
}
