using System.IO.Compression;
using System.Text;

namespace jvmcsharp.classpath
{
    internal class ZipEntry(string path) : IEntry
    {
        public string AbsPath { get; private set; } = Path.GetFullPath(path);

        public (byte[], IEntry) ReadClass(string className)
        {
            using (var archive = ZipFile.OpenRead(AbsPath))
            {
                foreach (var entry in archive.Entries)
                {
                    if (entry.FullName != className) continue;
                    using var stream = entry.Open();
                    using var reader = new StreamReader(stream);
                    return (Encoding.UTF8.GetBytes(reader.ReadToEnd()), this);
                }
            }
            throw new Exception($"class not found: {className}");
        }

        public override string ToString() => AbsPath;
    }
}
