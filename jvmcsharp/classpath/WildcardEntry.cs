namespace jvmcsharp.classpath
{
    internal class WildcardEntry : CompositeEntry
    {
        public WildcardEntry(string pathList) : base()
        {
            var baseDir = pathList[0..^1];
            foreach (var path in Directory.GetFiles(baseDir))
            {
                if (path.EndsWith(".jar", StringComparison.CurrentCultureIgnoreCase))
                {
                    var jarEntry = new ZipEntry(path);
                    Entries.Add(jarEntry);
                }
            }
        }
    }
}
