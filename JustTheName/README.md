# Just The Name

A Visual Studio extension that copies just the file name, without its path, to the
clipboard. `C:\src\App\Program.cs` becomes `Program.cs`.

The command **Copy File Name** sits right after **Copy Full Path** in:

- the context menu of a file in Solution Explorer
- the context menu of a document tab

Built on the [VisualStudio.Extensibility](https://learn.microsoft.com/visualstudio/extensibility/visualstudio.extensibility/)
SDK and runs out of process. Requires Visual Studio 2022 17.14 or later.

## Build

```
dotnet build JustTheName/JustTheName.csproj -c Release -p:Platform=AnyCPU
```

The VSIX ends up in `JustTheName/bin/Release/net8.0-windows8.0/JustTheName.vsix`.

## Limitations

The out-of-process API reports only one selected item, so the command is disabled when
several files are selected in Solution Explorer.

## License

MIT, see [LICENSE](../LICENSE).
