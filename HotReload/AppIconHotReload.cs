// HotReload/AppIconHotReload.cs
using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using Avalonia.VisualTree;

namespace YunmaIcons.Avalonia.HotReload;

public static class AppIconHotReload
{
    public static void OnHotReload(Type[]? updatedTypes)
    {
        if (updatedTypes == null) return;

        var relevant = updatedTypes.Any(t =>
            t == typeof(AppIcon) ||
            t == typeof(AppIconRegistry) ||
            t == typeof(AppIconKind));

        if (!relevant) return;

        Dispatcher.UIThread.Post(RefreshAllIcons);
    }

    private static void RefreshAllIcons()
    {
        if (Application.Current?.ApplicationLifetime
            is not IClassicDesktopStyleApplicationLifetime desktop) return;

        foreach (var window in desktop.Windows)
            RefreshInTree(window);
    }

    private static void RefreshInTree(Visual? visual)
    {
        if (visual == null) return;
        if (visual is AppIcon icon) icon.ForceRefresh();
        foreach (var child in visual.GetVisualChildren())
            RefreshInTree(child);
    }
}