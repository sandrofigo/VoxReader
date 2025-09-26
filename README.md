# VoxReader

[![logo](https://raw.githubusercontent.com/sandrofigo/VoxReader/refs/heads/develop/images/icon.png)](https://github.com/sandrofigo/VoxReader)

[![latest](https://img.shields.io/nuget/v/VoxReader)](https://www.nuget.org/packages/VoxReader/)
[![openupm](https://img.shields.io/npm/v/com.sandrofigo.voxreader?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.sandrofigo.voxreader/)
[![downloads](https://img.shields.io/nuget/dt/VoxReader?color=blue)](https://www.nuget.org/packages/VoxReader/)
[![test](https://github.com/sandrofigo/VoxReader/actions/workflows/test.yml/badge.svg)](https://github.com/sandrofigo/VoxReader/actions/workflows/test.yml)

A C# library to read .vox files created with [MagicaVoxel](https://ephtracy.github.io/index.html?page=mv_main)

---

## Currently supported features

At the moment it is possible to read:
- the name, size, position and rotation of all models
- all individual voxels including their color and position
- the palette that was used for the model
- the notes stored in the palette
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
Vector3 position = voxels[0].GlobalPosition;
Color color = voxels[0].Color;

// Access palette of .vox file
IPalette palette = voxFile.Palette;

// Access raw data of a chunk
byte[] rawData = voxFile.Chunks[0].Content;
```

## Unity

The package is also available on [OpenUPM](https://openupm.com/packages/com.sandrofigo.voxreader/) for the Unity game engine.
Follow the [manual installation](https://openupm.com/packages/com.sandrofigo.voxreader/?subPage=readme#modal-manualinstallation) or the [CLI installation](https://openupm.com/packages/com.sandrofigo.voxreader/?subPage=readme#modal-commandlinetool) instructions on the OpenUPM page to install the package in your project.

## Extending the library

The file format specification made by [ephtracy](https://github.com/ephtracy) is available at [.vox file format](https://github.com/ephtracy/voxel-model)

Support this project with a ⭐️, report an issue or if you feel adventurous and would like to extend the functionality open a pull request.
