namespace jvmcsharp.classpath
{
    internal class Classpath
    {
        public IEntry BootClasspath { get; internal set; } = EmptyEntry.Instance;
        public IEntry ExtClasspath { get; internal set; } = EmptyEntry.Instance;
        public IEntry UserClasspath { get; internal set; } = EmptyEntry.Instance;

        public Classpath(string jreOption, string cpOption)
        {
            ParseBootAndExtClasspath(jreOption);
            ParseUserClasspath(cpOption);
        }

        public (byte[], IEntry) ReadClass(string className)
        {
            className = $"{className}.class";
            try
            {
                var bootClass = BootClasspath.ReadClass(className);
                if (bootClass.Item1.Length > 0)
                {
                    return bootClass;
                }
            }
            catch (ClassNotFoundException) { }

            try
            {
                var extClass = ExtClasspath.ReadClass(className);
                if (extClass.Item1.Length > 0)
                {
                    return extClass;
                }
            }
            catch (ClassNotFoundException) { }

            return UserClasspath.ReadClass(className);
        }

        public override string ToString() => UserClasspath?.ToString() ?? string.Empty;

        public void ParseBootAndExtClasspath(string jreOption)
        {
            string jreDir = GetJreDir(jreOption);
            // jre/lib/*
            var jreLibPath = Path.Combine(jreDir, "lib", "*");
            BootClasspath = new WildcardEntry(jreLibPath);
            // jre/lib/ext/*
            var jreExtPath = Path.Combine(jreDir, "lib", "ext", "*");
            ExtClasspath = new WildcardEntry(jreExtPath);
        }

        public void ParseUserClasspath(string cpOption)
        {
            if (string.IsNullOrEmpty(cpOption)) cpOption = ".";
            UserClasspath = IEntry.NewEntry(cpOption);
        }

        public static string GetJreDir(string jreOption)
        {
            if (!string.IsNullOrEmpty(jreOption) && Directory.Exists(jreOption))
            {
                return jreOption;
            }
            var jre = "jre";
            var thisFolder = $"./{jre}";
            if (File.Exists(thisFolder))
            {
                return thisFolder;
            }
            var javaHome = Environment.GetEnvironmentVariable("JAVA_HOME");
            if (javaHome != null)
            {
                return Path.Combine(javaHome, jre);
            }
            throw new Exception("Can not find jre folder!");
        }
    }
}
