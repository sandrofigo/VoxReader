# Changelog

<!--
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).
-->

## [Unreleased]

### Fixed

- Fixed release workflow permissions

## [5.0.4] - 2025-10-17

### Fixed

- Fixed global position offset in some scenarios

## [5.0.3] - 2025-09-27

### Changed

- Replaced `<PackageIconUrl>` with `<PackageIcon>` to specify package icon

## [5.0.2] - 2025-09-14

### Added

- Added README to package for nuget.org

## [5.0.1] - 2025-07-31

### Fixed

- Fixed an exception when trying to read a .vox file that contains a META chunk

## [5.0.0] - 2024-12-19

### Added

- Added support for model rotations (Thanks to @Oribow)

### Changed

- Voxel and model positions are now accessed via the `GlobalPosition` and `LocalPosition` property
- Model size is now accessed via the `GlobalSize` and `LocalSize` property

## [4.1.1] - 2023-05-20

### Changed

- Removed unused code when parsing models

## [4.1.0] - 2023-05-20

### Added

- Added support for exported .vox files that are not .vox project files

### Fixed

- Fixed the model count being zero when trying to parse an exported .vox file instead of a .vox project file

## [4.0.0] - 2023-03-24

### Added

- Added support for parsing palette notes
- Added method `Palette.GetColorsByNote` to get palette colors grouped by palette note texts
- Added method `Palette.GetColorIndicesByNote` to get mapped palette color indices grouped by palette note texts
- Added mapped color index to voxel

### Changed

- `Palette.Colors` now stores the mapped colors that are visible in the UI of MagicaVoxel instead of the raw colors parsed from the `.vox` file

### Fixed

- Fixed wrong colors in palette when an IMAP chunk exists

## [3.1.0] - 2023-01-03

### Added

- Added `GlobalPosition` property to `Voxel`

## [3.0.0] - 2023-01-03

### Added

- Parse model position
- Parse model name
- Better validation for Unity meta files in build pipeline

## [2.1.2] - 2023-01-03

### Fixed

- Match version in Unity package file with release version

## [2.1.1] - 2023-01-03

### Changed

- Renamed workflow jobs (GitHub Actions)

### Fixed

- Fixed `KeyNotFoundException` when trying to read a vox file containing an IMAP chunk

## [2.1.0] - 2022-12-30

### Added

- Added support for Source Link (https://github.com/dotnet/sourcelink)
- Enabled `ContinuousIntegrationBuild` property for server builds

## [2.0.6] - 2022-12-29

### Fixed

- Added missing Unity .meta files in subdirectories

## [2.0.5] - 2022-12-29

### Fixed

- Adding missing Unity .meta files

## [2.0.4] - 2022-12-29

### Added

- Support for OpenUPM package

## [2.0.3] - 2022-12-28

### Added

- Version verification for changelog file in build pipeline (GitHub Actions)

### Fixed

- Fixed path to icon in project file

## [2.0.2] - 2022-12-23

### Changed

- Removed GitLab CI/CD integration in favor of GitHub Actions

## [2.0.1] - 2021-07-24

### Added

- Enabled XML documentation

## [2.0.0] - 2021-07-18

### Added

- Added support for multiple models
- Added access to raw data for all chunk types

### Changed

- Simplified access to voxels and model properties
- Library targets .NET Standard 2.0

## [1.2.0] - 2019-09-08

### Changed

- Reorganized files and added more documentation to the code

## [1.1.0] - 2019-09-08

### Changed

- Moved chunk files into own directory

## [1.0.4] - 2019-09-08

### Added

- First release with working CI/CD