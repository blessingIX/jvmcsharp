using jvmcsharp.instructions;
using jvmcsharp.instructions.basis;
using jvmcsharp.instructions.references;
using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;
using Newtonsoft.Json;
using Thread = jvmcsharp.rtda.Thread;

namespace jvmcsharp
{
    internal class Interpreter
    {
        public static void Interpret(Method method, bool logInst, string[] args)
        {
            var thread = new Thread();
            var frame = thread.CraeteFrame(method);
            thread.PushFrame(frame);
            var jArgs = CreateArgsArray(method.Class!.Loader!, args);
            frame.LocalVars.Set<JavaObject>(0, jArgs);
            try
            {
                Loop(thread, logInst);
            }
            catch (Exception)
            {
                LogFrames(thread);
                throw;
            }
        }

        public static void Loop(Thread thread, bool logInst)
        {
            var reader = new BytecodeReader();
            while (true)
            {
                var frame = thread.PeekFrame();
                var pc = frame.NextPc;
                thread.Pc = pc;
                // decode
                reader.Reset(frame.Method.Code, pc);
                var opcode = reader.ReadUInt8();
                Instruction inst = InstructionFactory.Create(opcode);
                inst.FetchOperands(reader);
                frame.NextPc = reader.Pc;
                if (logInst)
                {
                    LogInstruction(frame, inst);
                }
                // execute
                inst.Execute(frame);
                if (thread.IsStackEmpty())
                {
                    break;
                }
            }
        }

        private static void LogInstruction(Frame frame, Instruction inst)
        {
            var method = frame.Method;
            var className = method.Class!.Name;
            var methodName = method.Name;
            var pc = frame.Thread.Pc;
            Console.WriteLine($"{className}.{methodName}() #{pc} {inst.GetType().Name} {JsonConvert.SerializeObject(inst)}");
        }

        private static void LogFrames(Thread thread)
        {
            while (!thread.IsStackEmpty())
            {
                var frame = thread.PopFrame();
                var method = frame.Method;
                var className = method.Class!.Name;
                Console.WriteLine($">> pc: {frame.NextPc} {className} {method.Name} {method.Descriptor}");
            }
        }

        private static ArrayObject CreateArgsArray(ClassLoader loader, string[] args)
        {
            var stringClass = loader.LoadClass("java/lang/String");
            var argsArr = stringClass.ArrayClass().NewArray((uint)args.Length);
            var jArgs = argsArr.Refs;
            for (int i = 0; i < jArgs.Length; i++)
            {
                jArgs[i] = StringPool.JavaString(loader, args[i]);
            }
            return argsArr;
        }
    }
}
