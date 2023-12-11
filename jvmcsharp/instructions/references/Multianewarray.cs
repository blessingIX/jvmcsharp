using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;

namespace jvmcsharp.instructions.references
{
    internal class MULTI_ANEW_ARRAY : Instruction
    {
        public ushort Index { get; internal set; }
        public byte Dimensions { get; internal set; }

        public override void Execute(Frame frame)
        {
            var cp = frame.Method.Class!.ConstantPool;
            var classRef = cp.Get<ClassRef>(Index);
            var arrClass = classRef.ResolveClass();
            var stack = frame.OperandStack;
            var counts = PopAndCheckCounts(stack, Dimensions);
            var arr = NewMultiDimensionalArray(counts, arrClass);
            stack.Push(arr);
        }

        public override void FetchOperands(BytecodeReader reader)
        {
            Index = reader.ReadUInt16();
            Dimensions = reader.ReadUInt8();
        }

        internal static int[] PopAndCheckCounts(OperandStack stack, int dimensions)
        {
            var counts = new int[dimensions];
            for (int i = 0; i < dimensions; i++)
            {
                counts[i] = stack.Pop<int>();
                if (counts[i] < 0)
                {
                    throw new Exception("java.lang.NegativeArraySizeException");
                }
            }
            return counts;
        }

        private static ArrayObject NewMultiDimensionalArray(int[] counts, Class arrClass)
        {
            var count = (uint)counts[0];
            var arr = arrClass.NewArray(count);
            if (counts.Length > 1)
            {
                var refs = arr.Refs;
                for (int i = 0; refs.Length > i; i++)
                {

                    refs[i] = NewMultiDimensionalArray(counts[1..^0], arrClass.ComponentClass());
                }
            }
            return arr;
        }
    }
}
