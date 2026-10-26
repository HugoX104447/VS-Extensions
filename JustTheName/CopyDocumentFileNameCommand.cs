// Copyright (C) 2026 Karsten Lamm-Heimrich
// SPDX-License-Identifier: MIT

using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;

namespace JustTheName;

[VisualStudioContribution]
internal class CopyDocumentFileNameCommand : Command
{
    public override CommandConfiguration CommandConfiguration => new(Constants.CommandDisplayName)
    {
        Placements = [CommandPlacements.DocumentTab],
        TooltipText = Constants.CommandTooltipText
    };

    public override async Task ExecuteCommandAsync(IClientContext context, CancellationToken ct)
    {
        var fileName = context[ClientContextKey.Shell.ActiveEditorFileName];
        if (string.IsNullOrEmpty(fileName)) {
            // Not every document reports a file name in the client context.
            using var textView = await context.GetActiveTextViewAsync(ct);
            fileName = textView?.Document.Uri.LocalPath;
        }

        if (fileName != null) {
            // Client context values can arrive quoted; file names never contain quotes.
            fileName = fileName.Trim('"');
            if (fileName.Length > 0)
                ClipboardText.Set(Path.GetFileName(fileName));
        }
    }
}
