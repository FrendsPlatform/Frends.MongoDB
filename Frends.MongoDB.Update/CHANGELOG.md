# Changelog

## [2.0.0] - 2026-11-03
### Added
- Added an **Options** tab with `ThrowErrorOnFailure` and `ErrorMessageOnFailure` fields for configurable error handling.
- **[Breaking change]** Task now re-throws original exception instead of creating a new exception
### Changed
- Upgraded the task and test project to .NET 8.

## [1.0.1] - 2023-11-23
### Fixed
- Fixed dll error when importing the Task to Frends by adding local dll reference to the project file.

## [1.0.0] - 2022-11-01
### Added
- Initial implementation
