namespace jvmcsharp.classpath
{
    internal interface IEntry
    {
        public static readonly string PathListSeparator = Path.PathSeparator.ToString();

        public (byte[], IEntry) ReadClass(string className);

        public static IEntry NewEntry(string path)
        {
            if (path.Contains(PathListSeparator))
            {
                return new CompositeEntry(path);
            }
            if (path.EndsWith('*'))
            {
                return new WildcardEntry(path);
            }
            if (path.EndsWith(".jar", StringComparison.CurrentCultureIgnoreCase)
                || path.EndsWith(".zip", StringComparison.CurrentCultureIgnoreCase))
            {
                return new ZipEntry(path);
            }
            return new DirEntry(path);
        }
    }

    internal readonly struct EmptyEntry : IEntry
    {
        public static readonly EmptyEntry Instance = new();
        private static readonly byte[] empty = [];

        public readonly (byte[], IEntry) ReadClass(string className)
        {
            return (empty, this);
        }
    }
}
