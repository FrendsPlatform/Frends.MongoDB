# Changelog

## [2.0.0] - 2026-11-03
### Added
- Added the `Options` tab with the `ThrowErrorOnFailure` and `ErrorMessageOnFailure` fields.

### Changed
- Updated compression dependencies to address known security vulnerabilities.
- **[Breaking change]** When `ThrowErrorOnFailure` is enabled, the task now rethrows the original exception instead of wrapping it in a new exception.

## [1.0.1] - 2023-11-22
### Fixed
- Fixed dll error when importing the Task to Frends by adding local dll reference to the project file.

## [1.0.0] - 2022-10-26
### Added
- Initial implementation
