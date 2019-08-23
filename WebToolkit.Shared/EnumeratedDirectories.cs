using System.Collections.Generic;
using System.IO;

namespace WebToolkit.Shared
{
    public class EnumeratedDirectories
    {
        public IEnumerable<DirectoryInfo> Directories { get; set; }
        public IEnumerable<FileInfo> Files { get; set; }
        public IEnumerable<BadDirectoryInfo> BadDirectories { get; set; }
    }
}