// Copyright (C) 2026 Karsten Lamm-Heimrich
// SPDX-License-Identifier: MIT

using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;

namespace JustTheName;

[VisualStudioContribution]
internal class CopySelectedFileNameCommand : Command
{
    public override CommandConfiguration CommandConfiguration => new(Constants.CommandDisplayName)
    {
        Placements = [
            CommandPlacements.SolutionExplorerItem,
            CommandPlacements.SolutionExplorerProject
        ],

        // There is no selection count in the client context; this relies on VS leaving the path
        // empty when several items are selected.
        EnabledWhen =
            ActivationConstraint.ClientContext(ClientContextKey.Shell.ActiveSelectionPath, ".+"),
        TooltipText = Constants.CommandTooltipText
    };

    public override async Task ExecuteCommandAsync(IClientContext context, CancellationToken ct)
    {
        var path = await context.GetSelectedPathAsync(ct);
        if (path.IsFile) {
            var fileName = path.LocalPath.Trim('"');
            if (fileName.Length > 0)
                ClipboardText.Set(Path.GetFileName(fileName));
        }
    }
}
