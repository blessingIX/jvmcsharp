namespace jvmcsharp
{
    internal struct Cmd
    {
        public bool HelpFlag { get; private set; }
        public bool VersionFlag { get; private set; }
        public string CpOption { get; private set; }
        public string XjreOption { get; private set; }
        public string Class { get; private set; }
        public string[] Args { get; private set; }

        internal static Cmd ParseCmd()
        {
            var cmd = new Cmd();
            var args = Environment.GetCommandLineArgs().Skip(1).ToList();
            Var(args, ["help", "?"], false, v => cmd.HelpFlag = v);
            Var(args, ["version"], false, v => cmd.VersionFlag = v);
            Var(args, ["classpath", "cp"], string.Empty, v => cmd.CpOption = v);
            Var(args, ["Xjre"], string.Empty, v => cmd.XjreOption = v);
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

        internal static void Var(List<string> args, string[] names, bool defalut, Action<bool> action)
        {
            foreach (var name in names)
            {
                var found = args.Remove($"-{name}");
                if (!found) continue;

                action(found || defalut);
                action = v => { };
            }
        }

        internal static void Var(List<string> args, string[] names, string defalut, Action<string> action)
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
