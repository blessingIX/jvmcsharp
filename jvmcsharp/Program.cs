using jvmcsharp.Main;

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
            Console.WriteLine($"classpath:{cmd.CpOption} class:{cmd.Class} args:[{string.Join(" ", cmd.Args)}]");
        }
    }
}
