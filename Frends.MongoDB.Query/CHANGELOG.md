# Changelog

## [2.0.0] - 2026-11-03
### Fixed
- Added new `Options` tab with `ThrowErrorOnFailure` and `ErrorMessageOnFailure` properties to control how query failures are returned or thrown.
- Added `Result.Error`, including `Message` and `AdditionalInfo`, for query failure details.
- Upgraded the Query task and its test project to .NET 8.
- **[Breaking change]** Task now re-throws original exception instead of creating a new exception with a custom message.

## [1.0.1] - 2023-11-23
### Fixed
- Fixed dll error when importing the Task to Frends by adding local dll reference to the project file.

## [1.0.0] - 2022-10-28
### Added
- Initial implementation
