using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SharpCompress.Archives;
using SharpCompress.Archives.Zip;
using SharpCompress.Common;

namespace VoxReader.UnitTests
{
    public static class Zip
    {
        public static IEnumerable<string> UnzipFilesFromSevenZipArchive(string archivePath)
        {
            string tempPath = Path.Combine(Path.GetTempPath(), nameof(VoxReader), Guid.NewGuid().ToString());
            
            Directory.CreateDirectory(tempPath);
            
            using ZipArchive archive = ZipArchive.Open(archivePath);

            foreach (ZipArchiveEntry entry in archive.Entries.Where(entry => !entry.IsDirectory))
            {
                entry.WriteToDirectory(tempPath, new ExtractionOptions
                {
                    ExtractFullPath = true,
                    Overwrite = true
                });

                yield return Path.Combine(tempPath, entry.Key);
            }
        }
    }
}