namespace jvmcsharp.Main
{
    internal struct Cmd
    {
        public bool HelpFlag { get; private set; }
        public bool VersionFlag { get; private set; }
        public string CpOption { get; private set; }
        public string Class { get; private set; }
        public string[] Args { get; private set; }

        internal static Cmd ParseCmd()
        {
            var cmd = new Cmd();
            var args = Environment.GetCommandLineArgs().Skip(1).ToList();
            Var(args, v => cmd.HelpFlag = v, ["help", "?"], false);
            Var(args, v => cmd.VersionFlag = v, ["version"], false);
            Var(args, v => cmd.CpOption = v, ["classpath", "cp"], string.Empty);
            if (args.Count > 0)
            {
                cmd.Class = args[0];
                cmd.Args = args.Skip(1).ToArray();
            }
            return cmd;
        }

        internal void PrintUsage()
        {
            Console.WriteLine($"Usage: {Environment.GetCommandLineArgs()[0]} [-options] class [args...]");
        }

        internal static void Var(List<string> args, Action<bool> action, string[] names, bool defalut)
        {
            foreach (var name in names)
            {
                var found = args.Remove($"-{name}");
                if (!found) continue;

                action(found || defalut);
                action = v => { };
            }
        }

        internal static void Var(List<string> args, Action<string> action, string[] names, string defalut)
        {
            foreach (var name in names)
            {
                var idx = args.IndexOf($"-{name}");
                if (idx < 0) continue;
                var value = args.Count > idx + 1 ? args[idx + 1] : string.Empty;
                action(value ?? defalut);
                action = v => { };
                if (idx < args.Count) args.RemoveAt(idx);
                if (idx < args.Count) args.RemoveAt(idx);
            }
        }
    }
}
