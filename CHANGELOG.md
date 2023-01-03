# Changelog

<!--
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).
-->

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

### Changed

- Simplified access to voxels and model properties
- Library targets .NET Standard 2.0

### Added

- Added support for multiple models
- Added access to raw data for all chunk types

## [1.2.0] - 2019-09-08

### Changed

- Reorganized files and added more documentation to the code

## [1.1.0] - 2019-09-08

### Changed

- Moved chunk files into own directory

## [1.0.4] - 2019-09-08

### Added

- First release with working CI/CD
