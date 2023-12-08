using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;

namespace jvmcsharp.instructions.references
{
    internal class PUT_FIELD : Index16Instruction
    {
        public override void Execute(Frame frame)
        {
            var currentMethod = frame.Method;
            var currentClass = currentMethod.Class!;
            var cp = currentClass.ConstantPool;
            var fieldRef = cp.Get<FieldRef>(Index);
            var field = fieldRef.ResolveField();
            var @class = field.Class;

            if (field.IsStatic())
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
            var stack = frame.OperandStack;
            switch (descriptor[0])
            {
                case 'Z':
                case 'B':
                case 'C':
                case 'S':
                case 'I':
                    var intVal = stack.Pop<int>();
                    var refIntCase = GetNonNullReference(stack);
                    refIntCase.Fields.Set(slotId, intVal);
                    break;
                case 'F':
                    var floatVal = stack.Pop<float>();
                    var refFloatCase = GetNonNullReference(stack);
                    refFloatCase.Fields.Set(slotId, floatVal);
                    break;
                case 'J':
                    var longVal = stack.Pop<long>();
                    var refLongCase = GetNonNullReference(stack);
                    refLongCase.Fields.Set(slotId, longVal);
                    break;
                case 'D':
                    var doubleVal = stack.Pop<double>();
                    var refDoubleCase = GetNonNullReference(stack);
                    refDoubleCase.Fields.Set(slotId, doubleVal);
                    break;
                case 'L':
                case '[':
                    var refVal = stack.Pop<JavaObject>();
                    var refRefCase = GetNonNullReference(stack);
                    refRefCase.Fields.Set(slotId, refVal);
                    break;
            }
        }

        public static JavaObject GetNonNullReference(OperandStack stack) => stack.Pop<JavaObject>() ?? throw new Exception("java.lang.NullPointException");
    }
}
