// Copyright (C) 2026 Karsten Lamm-Heimrich
// SPDX-License-Identifier: MIT

using Microsoft.VisualStudio.Extensibility.Commands;

namespace JustTheName;

// Places the commands in the groups that hold "Copy Full Path".
internal static class CommandPlacements
{
    private static readonly Guid GuidShlMainMenu = new("d309f791-903f-11d0-9efc-00a0c911004f");

    // Copy Full Path is 0x0080, Open Folder in File Explorer 0x0100.
    private const uint IdgVsContextItemExplore = 0x02E6;

    // Copy Full Path is 0x0080 in both.
    private const uint IdgVsContextSolutionExplore = 0x0265;
    private const uint IdgVsContextProjectExplore = 0x0266;

    // Copy Full Path is 0x0180, Copy Relative Path 0x0181, Open in File Explorer 0x0190.
    private const uint IdgVsEzDocWindowPathOps = 0x01ED;

    public static CommandPlacement SolutionExplorerItem
        => CommandPlacement.VsctParent(GuidShlMainMenu, IdgVsContextItemExplore, 0x0081);

    public static CommandPlacement SolutionExplorerProject
        => CommandPlacement.VsctParent(GuidShlMainMenu, IdgVsContextProjectExplore, 0x0081);

    public static CommandPlacement SolutionExplorerSolution
        => CommandPlacement.VsctParent(GuidShlMainMenu, IdgVsContextSolutionExplore, 0x0081);

    public static CommandPlacement DocumentTab
        => CommandPlacement.VsctParent(GuidShlMainMenu, IdgVsEzDocWindowPathOps, 0x0182);
}
