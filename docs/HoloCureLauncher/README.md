# HoloCureLauncher Documentation

Contains a compilable (but non-functional) project containing the fully, extensive documentation of member definitions included in the official, proprietary HoloCureLauncher.

Member documentation details under what circumstances their values are changed or access, they're invoked, etc., and what purpose they serve.

Member definitions are decompiled by ILSpy from the proprietary binary and are subject to the same license as the source program. Documentation is licensed under the GPL v2 license (see `LICENSE` in the repository root).

## Notes

- The proprietary launcher is distributed as a single executable file for Windows as a .NET 6.0 bundle. This project is not packaged or distributed at all, let alone as a bundle.
- The proprietary launcher and this project both use .NET 6.0 with the `net6.0-windows` framework target.
- The project file (`.csproj`) used to build this project is not an accurate repreentation of the project file used to compile the proprietary launcher.
  - Accurate representation is not a necessity and never a guarantee regardless.
- The `TRACE` `#define`d in `HoloCureLauncher.Helpers.Downloader` is also `#define`d in the distributed proprietary binaries.
- Exposed members are all products of decompilation.
  - Method bodies and field/property initializers have been manually excluded.
  - Regions (such as the `Compiler-generated` `#region`s) are manually included for code clarity.
  - The `External definitions` `#region` is not part of the source program and are included by this project for better editor support
