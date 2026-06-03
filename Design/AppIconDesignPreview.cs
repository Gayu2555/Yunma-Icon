// Design/AppIconDesignPreview.cs
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Platform;

namespace YunmaIcons.Avalonia.Design;

/// <summary>
/// Design-time preview provider for AppIcon.
/// Automatically registered via ProvideDesignTimeData attribute.
/// </summary>
public static class AppIconDesignPreview
{
    public static AppIcon CreateSample(AppIconKind kind, double size = 24)
    {
        return new AppIcon
        {
            Kind = kind,
            Size = size,
            Foreground = Brushes.White
        };
    }
}