namespace jvmcsharp.classpath
{
    internal class CompositeEntry : IEntry
    {
        public List<IEntry> Entries { get; protected set; } = [];

        protected CompositeEntry() { }

        public CompositeEntry(string pathList)
        {
            foreach (var path in pathList.Split(IEntry.PathListSeparator))
            {
                Entries.Add(IEntry.NewEntry(path));
            }
        }

        public virtual (byte[], IEntry) ReadClass(string className)
        {
            foreach (var entry in Entries)
            {
                try
                {
                    var @class = entry.ReadClass(className);
                    if (@class.Item1.Length == 0) continue;
                    return (@class.Item1, @class.Item2);
                }
                catch (Exception)
                {
                    continue;
                }
            }
            throw new Exception($"class not found: {className}");
        }

        public override string ToString() => string.Join(IEntry.PathListSeparator, Entries);
    }
}
