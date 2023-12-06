using jvmcsharp.instructions;
using jvmcsharp.instructions.basis;
using Newtonsoft.Json;
using MemberInfo = jvmcsharp.classfile.MemberInfo;
using Thread = jvmcsharp.rtda.Thread;

namespace jvmcsharp
{
    internal class Interpreter
    {
        public static void Interpret(MemberInfo methodInfo)
        {
            var codeAttr = methodInfo.CodeAttribute();
            var maxLocals = codeAttr.MaxLocals;
            var maxStack = codeAttr.MaxStack;
            var bytecode = codeAttr.Code;
            var thread = new Thread();
            var frame = thread.CraeteFrame(maxLocals, maxStack);
            thread.PushFrame(frame);
            try
            {
                Loop(thread, bytecode);
            }
            /*catch (Exception ex)
            {
                if (!ex.Message.StartsWith("Unsupported opcode"))
                    throw;
            }*/
            finally
            {
                Console.WriteLine($"""
                    {nameof(frame.LocalVars)}: {JsonConvert.SerializeObject(frame.LocalVars)}
                    {nameof(frame.OperandStack)}: {JsonConvert.SerializeObject(frame.OperandStack)}
                    """);
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
                Console.WriteLine($"pc: {pc:X4} inst: {inst.GetType().Name} {JsonConvert.SerializeObject(inst)}");
                inst.Execute(frame);
            }
        }
    }
}
