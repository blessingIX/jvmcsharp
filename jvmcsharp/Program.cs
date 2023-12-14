using jvmcsharp.classfile;
using jvmcsharp.classpath;
using jvmcsharp.native;
using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;

namespace jvmcsharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cmd = Cmd.ParseCmd();
            if (cmd.VersionFlag)
            {
                Console.WriteLine("version 0.0.1");
            }
            else if (cmd.HelpFlag || string.IsNullOrEmpty(cmd.Class))
            {
                cmd.PrintUsage();
            }
            else
            {
                StartJVM(cmd);
            }
        }

        static void StartJVM(Cmd cmd)
        {
            Registry.RegisterNativeMethods();
            var cp = new Classpath(cmd.XjreOption, cmd.CpOption);
            Console.WriteLine($"classpath: {cp} class: {cmd.Class} args: [{string.Join(", ", cmd.Args)}]");
            var classLoader = new ClassLoader(cp) { VerboseFlag = cmd.VerboseClassFlag };
            var className = cmd.Class.Replace('.', '/');
            var mainClass = classLoader.LoadClass(className);
            var mainMethod = mainClass.GetMainMethod();
            if (mainMethod != null)
            {
                Interpreter.Interpret(mainMethod, cmd.VerboseInstFlag, cmd.Args);
            }
            else
            {
                Console.WriteLine($"Main method not found in class {cmd.Class}");
            }
        }

        static void TestRtda()
        {
            /*Console.WriteLine("运行时数据区测试");
            var frame = new Frame(null!, 100, 100);
            TestLocalVars(frame.LocalVars);
            TestOperandStack(frame.OperandStack);*/
        }

        static ClassFile LoadClass(string className, Classpath cp)
        {
            var (classData, _) = cp.ReadClass(className);
            // Console.WriteLine($"classData: [{BitConverter.ToString(classData).Replace("-", ", ")}]");
            return new ClassFile(classData);
        }

        static void PrintClassInfo(ClassFile cf)
        {
            Console.WriteLine($"""
                version: {cf.MajorVersion}.{cf.MinorVersion}
                constants count: {cf.ConstantPool.Length}
                access flags: {BitConverter.ToString(BitConverter.GetBytes(cf.AccessFlags)).Replace('-', ' ')}
                this class: {cf.ClassName()}
                super class: {cf.SuperClassName()}
                interfaces: [{string.Join(", ", cf.InterfaceNames())}]
                fields count: {cf.Fileds.Length}
                {$"\t{string.Join("\n\t", cf.Fileds.Select(v => v.Name()))}"}
                methods count: {cf.Methods.Length}
                {$"\t{string.Join("\n\t", cf.Methods.Select(v => v.Name()))}"}
                """);
        }

        static void TestLocalVars(LocalVars localVars)
        {
            localVars.Set(0, 100);
            localVars.Set(1, -100);
            localVars.Set(2, 2997924580L);
            localVars.Set(4, -2997924580L);
            localVars.Set(6, 3.1415626f);
            localVars.Set(7, 2.71828182845d);
            localVars.Set<JavaObject>(9, null!);
            Console.WriteLine($"""
                {localVars.Get<int>(0)}
                {localVars.Get<int>(1)}
                {localVars.Get<long>(2)}
                {localVars.Get<long>(4)}
                {localVars.Get<float>(6)}
                {localVars.Get<double>(7)}
                {localVars.Get<JavaObject>(9)}
                """);
        }

        static void TestOperandStack(OperandStack operandStack)
        {
            operandStack.Push(100);
            operandStack.Push(-100);
            operandStack.Push(2997924580L);
            operandStack.Push(-2997924580L);
            operandStack.Push(3.1415626f);
            operandStack.Push(2.71828182845d);
            operandStack.Push<JavaObject>(null!);
            Console.WriteLine($""""
                {operandStack.Pop<JavaObject>()}
                {operandStack.Pop<double>()}
                {operandStack.Pop<float>()}
                {operandStack.Pop<long>()}
                {operandStack.Pop<long>()}
                {operandStack.Pop<int>()}
                {operandStack.Pop<int>()}
                """");
        }
    }
}
