using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WebToolkit.Contracts.Providers;
using WebToolkit.Shared;

namespace WebToolkit.Common.Providers
{
    public class FileProvider : IFileProvider
    {
        public bool DirectoryExists(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

        public bool FileExists(string fileName)
        {
            return File.Exists(fileName);
        }

        public FileInfo GetFileInfo(string fileName)
        {
            if(FileExists(fileName))
                return new FileInfo(fileName);

            throw new ArgumentNullException(nameof(fileName), "File not found");
        }

        public DirectoryInfo GetDirectoryInfo(string directoryPath)
        {
            if(DirectoryExists(directoryPath))
                return new DirectoryInfo(directoryPath);

            throw new ArgumentNullException(nameof(directoryPath), "Directory not found");
        }

        public async Task<IEnumerable<byte>> GetFileContents(string filename, FileMode fileMode)
        {
            var fileInfo = GetFileInfo(filename);
            var buffer = new byte[fileInfo.Length];
            using (var fileStream = fileInfo.Open(fileMode))
            {
                await fileStream.WriteAsync(buffer, 0, buffer.Length);
            }

            return buffer;
        }


        public async Task<EnumeratedDirectories> EnumerateDirectories(string directoryPath, bool recursive)
        {
            var directoryList = new List<DirectoryInfo>();
            var badDirectoryList = new List<BadDirectoryInfo>();

            var currentDirectoryInfo = GetDirectoryInfo(directoryPath);

            var currentDirectory = currentDirectoryInfo.FullName;

            try
            {
                foreach (var directoryInfo in currentDirectoryInfo.GetDirectories())
                {
                    currentDirectory = directoryInfo.Name;
                    directoryList.Add(directoryInfo);

                    if (!recursive) continue;

                    var enumeratedSubDirectories = await EnumerateDirectories(directoryInfo.FullName, true);
                    directoryList.AddRange(enumeratedSubDirectories.Directories);
                    badDirectoryList.AddRange(enumeratedSubDirectories.BadDirectories);

                }
            }
            catch (IOException exception)
            {
                badDirectoryList.Add(new BadDirectoryInfo
                {
                    DirectoryName = currentDirectory,
                    Exception =  exception
                });
            }

            return new EnumeratedDirectories
            {
                BadDirectories = badDirectoryList.ToArray(),
                Directories = directoryList.ToArray()
            };
        }
    }
}