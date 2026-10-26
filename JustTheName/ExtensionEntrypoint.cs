// Copyright (C) 2026 Karsten Lamm-Heimrich
// SPDX-License-Identifier: MIT

using Microsoft.VisualStudio.Extensibility;

namespace JustTheName;

[VisualStudioContribution]
internal class ExtensionEntrypoint : Extension
{
    public override ExtensionConfiguration ExtensionConfiguration => new()
    {
        Metadata = new ExtensionMetadata(
            id: "JustTheName.2a43edf1-af36-4404-a8f6-11935175fa3a",
            version: ExtensionAssemblyVersion,
            publisherName: "Karsten Lamm-Heimrich",
            displayName: "Just The Name",
            description: "Copies just the file name, without its path, to the clipboard.")
        {
            Preview = false,

            // Visual Studio 2022 hosts extensions on .NET 8, Visual Studio 18 also on .NET 10.
            DotnetTargetVersions = ["net8.0", "net10.0"]
        }
    };
}
