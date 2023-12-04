using jvmcsharp.classfile;
using jvmcsharp.classpath;

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
            var cp = new Classpath(cmd.XjreOption, cmd.CpOption);
            Console.WriteLine($"classpath: {cp} class: {cmd.Class} args: [{string.Join(", ", cmd.Args)}]");
            var className = cmd.Class.Replace(".", "/");
            var cf = LoadClass(className, cp);
            Console.WriteLine(cmd.Class);
            PrintClassInfo(cf);
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
    }
}
