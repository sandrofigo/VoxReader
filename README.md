# VoxReader

![latest](https://img.shields.io/nuget/v/VoxReader)
![build](https://img.shields.io/appveyor/ci/sandrofigo/voxreader)

A C# library to read .vox files created with MagicaVoxel

---

## Currently supported features

At the moment it is possible to read:
- the size of the model
- all individual voxels including their color and position
- the palette that was used for the model

## Usage

```csharp
using System;
using System.IO;
using System.Linq;
using VoxReader;

class Program
{
    public static void Main(string[] args)
    {
        var data = File.ReadAllBytes("my_awesome_model.vox");

        var chunks = Reader.GetChunks(data);

        var sizeChunk = chunks.FirstOrDefault(c => c.Id == nameof(ChunkType.SIZE)) as SizeChunk;
        Console.WriteLine(sizeChunk?.ToString());

        var voxelChunk = chunks.FirstOrDefault(c => c.Id == nameof(ChunkType.XYZI)) as VoxelChunk;
        Console.WriteLine(voxelChunk?.ToString());

        var paletteChunk = chunks.FirstOrDefault(c => c.Id == nameof(ChunkType.RGBA)) as PaletteChunk;
        Console.WriteLine(paletteChunk?.ToString());
    }
}
```

## Extending the library

Only .vox files that contain a single model are supported for now.

The file format specification made by [ephtracy](https://github.com/ephtracy) is available at [.vox file format](https://github.com/ephtracy/voxel-model/blob/master/MagicaVoxel-file-format-vox.txt)

Support this project with a ⭐️, report an issue or if you feel adventurous and would like to extend the functionality open a pull request.
