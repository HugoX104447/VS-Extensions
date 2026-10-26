// Copyright (C) 2026 Karsten Lamm-Heimrich
// SPDX-License-Identifier: MIT

using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;
using Microsoft.VisualStudio.ProjectSystem.Query;

namespace JustTheName;

// The client context has no selection path for the solution node, so the file name comes
// from the project query API instead.
[VisualStudioContribution]
internal class CopySolutionFileNameCommand : Command
{
    public override CommandConfiguration CommandConfiguration => new(Constants.CommandDisplayName)
    {
        Placements = [CommandPlacements.SolutionExplorerSolution],
        EnabledWhen = ActivationConstraint.SolutionState(SolutionState.Exists),
        TooltipText = Constants.CommandTooltipText
    };

    public override async Task ExecuteCommandAsync(IClientContext context, CancellationToken ct)
    {
        var solutions = await Extensibility.Workspaces().QuerySolutionAsync(
            solution => solution.With(s => s.FileName), ct);

        var fileName = solutions.FirstOrDefault()?.FileName;
        if (!string.IsNullOrEmpty(fileName))
            ClipboardText.Set(fileName);
    }
}
