# Changelog

## [2.0.0](https://github.com/fransiscuss/ampeco-dotnet/compare/v1.0.0...v2.0.0) (2026-09-12)


### ⚠ BREAKING CHANGES

* the package ID is now AmpecoDotNet.Sdk, net9.0 is no longer a target framework, and AmpecoClientOptions.TenantUrl/ApiKey are validated at runtime rather than being `required` members.

### Features

* modernize packaging, CI and release flow ([b800c01](https://github.com/fransiscuss/ampeco-dotnet/commit/b800c01a0f90ccc581270eedb24df259e6de97bd))


### Bug Fixes

* correct legacy pagination, HttpClient ownership and error messages ([f43349d](https://github.com/fransiscuss/ampeco-dotnet/commit/f43349d303f6666971ebe190d601aca236d955eb))
* reject a RequestTimeout that HttpClient itself rejects ([f1653b1](https://github.com/fransiscuss/ampeco-dotnet/commit/f1653b14b5d90d04cc01d2dfb71c79f3da47fc22))

## 1.0.0 (2026-09-05)


### Features

* add MIT license and seed automated release flow ([3401ac6](https://github.com/fransiscuss/ampeco-dotnet/commit/3401ac68f27ae03fc6696724ee089a71fc3b2f43))
