using jvmcsharp.instructions;
using jvmcsharp.instructions.basis;
using jvmcsharp.rtda.heap;
using Thread = jvmcsharp.rtda.Thread;

namespace jvmcsharp
{
    internal class Interpreter
    {
        public static void Interpret(Method method)
        {
            var thread = new Thread();
            var frame = thread.CraeteFrame(method);
            thread.PushFrame(frame);
            try
            {
                Loop(thread, method.Code);
            }
            finally
            {
                /*Console.WriteLine($"""
                    {nameof(frame.LocalVars)}: {JsonConvert.SerializeObject(frame.LocalVars)}
                    {nameof(frame.OperandStack)}: {JsonConvert.SerializeObject(frame.OperandStack)}
                    """);*/
            }
        }

        public static void Loop(Thread thread, byte[] bytecode)
        {
            var frame = thread.PopFrame();
            var reader = new BytecodeReader();
            while (true)
            {
                var pc = frame.NextPc;
                thread.Pc = pc;
                // decode
                reader.Reset(bytecode, pc);
                var opcode = reader.ReadUInt8();
                Instruction inst = InstructionFactory.Create(opcode);
                inst.FetchOperands(reader);
                frame.NextPc = reader.Pc;
                // execute
                // Console.WriteLine($"pc: {pc:X4} inst: {inst.GetType().Name} {JsonConvert.SerializeObject(inst)}");
                inst.Execute(frame);
            }
        }
    }
}
