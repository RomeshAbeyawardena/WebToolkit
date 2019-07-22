using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WebToolkit.Shared;

namespace WebToolkit.Contracts.Providers
{
    public interface IFileProvider
    {
        bool DirectoryExists(string directoryPath);
        bool FileExists(string fileName);
        FileInfo GetFileInfo(string fileName);
        DirectoryInfo GetDirectoryInfo(string directoryPath);
        Task<IEnumerable<byte>> GetFileContents(string filename, FileMode fileMode);
        Task<EnumeratedDirectories> EnumerateDirectories(string directoryPath, bool recursive, bool getFiles);
    }
}