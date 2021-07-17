# VoxReader

[![latest](https://img.shields.io/nuget/v/VoxReader)](https://www.nuget.org/packages/VoxReader/)
[![build](https://gitlab.com/sandrofigo/VoxReader/badges/master/pipeline.svg?key_text=build)](https://gitlab.com/sandrofigo/VoxReader/-/pipelines?page=1&scope=all&ref=master)
A C# library to read .vox files created with MagicaVoxel

---

## Currently supported features

At the moment it is possible to read:
- the size of all models
- all individual voxels including their color and position
- the palette that was used for the model
- all raw data related to the scene
- miscellaneous raw data

## Usage

```csharp
// Read .vox file
IVoxFile voxFile = VoxReader.ReadVoxFile(data);

// Access models of .vox file
IModel[] models = voxFile.Models;

// Access voxels of first model in the file
Voxel[] voxels = models[0].Voxels;

// Access properties of a voxel
Vector3 position = voxels[0].Position;
Color color = voxels[0].Color;

// Access palette of .vox file
IPalette palette = voxFile.Palette;

// Access raw data of a chunk
byte[] rawData = voxFile.Chunks[0].Content;
```

## Extending the library

The file format specification made by [ephtracy](https://github.com/ephtracy) is available at [.vox file format](https://github.com/ephtracy/voxel-model/blob/master/MagicaVoxel-file-format-vox.txt)

Support this project with a ⭐️, report an issue or if you feel adventurous and would like to extend the functionality open a pull request.
