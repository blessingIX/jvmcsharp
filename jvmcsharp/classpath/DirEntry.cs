namespace jvmcsharp.classpath
{
    internal class DirEntry(string path) : IEntry
    {
        public string AbsDir { get; private set; } = Path.GetFullPath(path);

        public (byte[], IEntry) ReadClass(string className)
        {
            var fileName = Path.Combine(AbsDir, className);
            if (File.Exists(fileName))
            {
                return (File.ReadAllBytes(fileName), this);
            }
            return (Array.Empty<byte>(), this);
        }

        public override string ToString() => AbsDir;
    }
}
