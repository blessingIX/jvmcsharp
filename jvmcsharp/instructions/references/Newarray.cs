using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;

namespace jvmcsharp.instructions.references
{
    internal class NEW_ARRAY : Instruction
    {
        public byte Atype { get; internal set; }

        public override void Execute(Frame frame)
        {
            var stack = frame.OperandStack;
            var count = stack.Pop<int>();
            if (count < 0)
            {
                throw new Exception("java.lang.NegativeArraySizeException");
            }
            var classLoader = frame.Method.Class!.Loader;
            var arrClass = GetPrimitiveArrayClass(classLoader!, Atype);
            var arr = arrClass.NewArray((uint)count);
            stack.Push<JavaObject>(arr);
        }

        public override void FetchOperands(BytecodeReader reader) => Atype = reader.ReadUInt8();

        private static Class GetPrimitiveArrayClass(ClassLoader loader, byte atype)
        {
            return atype switch
            {
                (byte)AT.AT_BOOLEAN => loader.LoadClass("[Z"),
                (byte)AT.AT_BYTE => loader.LoadClass("[B"),
                (byte)AT.AT_CHAR => loader.LoadClass("[C"),
                (byte)AT.AT_SHORT => loader.LoadClass("[S"),
                (byte)AT.AT_INT => loader.LoadClass("[I"),
                (byte)AT.AT_LONG => loader.LoadClass("[J"),
                (byte)AT.AT_FLOAT => loader.LoadClass("[F"),
                (byte)AT.AT_DOUBLE => loader.LoadClass("[D"),
                _ => throw new Exception("Invalid atype")
            };
        }
    }

    internal enum AT : byte
    {
        AT_BOOLEAN = 4,
        AT_CHAR = 5,
        AT_FLOAT = 6,
        AT_DOUBLE = 7,
        AT_BYTE = 8,
        AT_SHORT = 9,
        AT_INT = 10,
        AT_LONG = 11,
    }
}
