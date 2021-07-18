# VoxReader

[![latest](https://img.shields.io/nuget/v/VoxReader)](https://www.nuget.org/packages/VoxReader/)
[![downloads](https://img.shields.io/nuget/dt/VoxReader?color=blue)](https://www.nuget.org/packages/VoxReader/)
[![pipeline status](https://gitlab.com/sandrofigo/VoxReader/badges/develop/pipeline.svg)](https://gitlab.com/sandrofigo/VoxReader/-/commits/develop)

A C# library to read .vox files created with [MagicaVoxel](https://ephtracy.github.io/index.html?page=mv_main)

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
IVoxFile voxFile = VoxReader.Read("my_awesome_model.vox");

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
