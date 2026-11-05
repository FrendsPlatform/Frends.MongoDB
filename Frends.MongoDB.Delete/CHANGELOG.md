# Changelog

## [2.0.0] - 2026-11-04
### Added
- Added an **Options** tab with `ThrowErrorOnFailure` (default: `true`) and `ErrorMessageOnFailure` fields for configurable error handling.
- Added support for .NET 8.
### Changed
- **[Breaking change]** When `ThrowErrorOnFailure` is enabled and `ErrorMessageOnFailure` is empty, the Task now re-throws the original exception instead of wrapping it in a new exception.

## [1.0.1] - 2023-11-22
### Fixed
- Fixed dll error when importing the Task to Frends by adding local dll reference to the project file.

## [1.0.0] - 2022-10-31
### Added
- Initial implementation
