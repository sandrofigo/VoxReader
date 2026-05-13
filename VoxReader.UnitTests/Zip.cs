using System.IO;
using System.Linq;
using SharpCompress.Archives;
using SharpCompress.Archives.Zip;

namespace VoxReader.UnitTests;

public static class Zip
{
    public static byte[] GetBytesFromFirstFileInArchive(string archivePath)
    {
        using var archive = ZipArchive.OpenArchive(archivePath);

        IArchiveEntry firstFile = archive.Entries.First(entry => !entry.IsDirectory);

        using var memoryStream = new MemoryStream();
        firstFile.WriteTo(memoryStream);

        return memoryStream.ToArray();
    }
}