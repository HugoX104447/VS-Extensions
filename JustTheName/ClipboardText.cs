// Copyright (C) 2026 Karsten Lamm-Heimrich
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

namespace JustTheName;

// The extension runs out of process without WPF or WinForms, so the clipboard is set through the
// Win32 API directly.
internal static partial class ClipboardText
{

    public static void Set(string text)
    {
        if (!TryOpenClipboard())
            return;

        try {
            _ = EmptyClipboard();

            const uint gMemMovable = 0x0002;

            var bytes = (text.Length + 1) * sizeof(char);
            var handle = GlobalAlloc(gMemMovable, (nuint)bytes);
            if (handle == IntPtr.Zero)
                return;

            var target = GlobalLock(handle);
            if (target == IntPtr.Zero) {
                _ = GlobalFree(handle);
                return;
            }

            Marshal.Copy(text.ToCharArray(), 0, target, text.Length);
            Marshal.WriteInt16(target, text.Length * sizeof(char), 0);
            _ = GlobalUnlock(handle);

            const uint cfUnicodeText = 13;

            // On success the clipboard owns the memory.
            if (SetClipboardData(cfUnicodeText, handle) == IntPtr.Zero)
                GlobalFree(handle);
        } finally {
            _ = CloseClipboard();
        }
    }

    // Another process may hold the clipboard for a moment.
    private static bool TryOpenClipboard()
    {
        for (var attempt = 0; attempt < 10; ++attempt) {
            if (OpenClipboard(IntPtr.Zero))
                return true;
            Thread.Sleep(20);
        }
        return false;
    }

    [LibraryImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool OpenClipboard(IntPtr hWndNewOwner);

    [LibraryImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool CloseClipboard();

    [LibraryImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool EmptyClipboard();

    [LibraryImport("user32.dll", SetLastError = true)]
    private static partial IntPtr SetClipboardData(uint uFormat, IntPtr hMem);

    [LibraryImport("kernel32.dll", SetLastError = true)]
    private static partial IntPtr GlobalAlloc(uint uFlags, nuint dwBytes);

    [LibraryImport("kernel32.dll", SetLastError = true)]
    private static partial IntPtr GlobalLock(IntPtr hMem);

    [LibraryImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool GlobalUnlock(IntPtr hMem);

    [LibraryImport("kernel32.dll", SetLastError = true)]
    private static partial IntPtr GlobalFree(IntPtr hMem);
}
