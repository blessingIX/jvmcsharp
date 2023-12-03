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
            try
            {
                var (classData, entry) = cp.ReadClass(className);
                Console.WriteLine($"classData: [{string.Join(", ", classData)}]");
            }
            catch
            {
                Console.WriteLine($"Could not find or load main class {cmd.Class}");
            }
        }
    }
}
