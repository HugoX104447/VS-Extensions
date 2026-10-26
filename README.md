# VS-Extensions

Small Visual Studio extensions, each doing one thing well. All of them are built on the
[VisualStudio.Extensibility](https://learn.microsoft.com/visualstudio/extensibility/visualstudio.extensibility/)
SDK and run out of process, isolated from the Visual Studio process.

## Extensions

| Extension | What it does | Where to find it |
|-----------|--------------|------------------|
| [Just The Name](JustTheName/README.md) | Copies just the file name, without its path, to the clipboard: `C:\src\App\Program.cs` becomes `Program.cs`. | **Copy File Name** right after **Copy Full Path** in the context menu of a Solution Explorer file and of a document tab |

## Requirements

- Visual Studio 2022 17.14 or later to run the extensions, including Visual Studio 18.
  Community, Professional and Enterprise are all supported.
- .NET SDK 9.0.200 or later to build them; earlier SDKs cannot read the `.slnx` solution.
  The extensions are built for .NET 8 and declare support for .NET 8 and .NET 10:
  Visual Studio 2022 hosts extensions on .NET 8, Visual Studio 18 also on .NET 10.

The only package dependencies are the VisualStudio.Extensibility SDK and its build tools
from NuGet. The legacy VSSDK is not used.

## Build

```
dotnet build Extensions.slnx -c Release
```

Each extension ends up as a `.vsix` in its `bin/Release/net8.0-windows8.0` folder.
Double-click the file to install it. With the *Visual Studio extension development*
workload installed, opening the solution in Visual Studio and pressing F5 debugs an
extension in an experimental instance.

## Repository layout

```
Extensions.slnx        all extensions in one solution
.editorconfig          code style, enforced in the build
JustTheName/           one folder per extension, with its own README
```

The code style rules in `.editorconfig` are enforced during the build
(`EnforceCodeStyleInBuild`), so style violations show up as warnings.

## License

MIT, see [LICENSE](LICENSE).
