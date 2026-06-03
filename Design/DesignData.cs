// Design/DesignData.cs
using Avalonia.Controls;
using Avalonia.Media;

namespace YunmaIcons.Avalonia.Design;

/// <summary>
/// Static design-time data — dipakai di d:DataContext atau d:DesignInstance
/// </summary>
public static class DesignData
{
    public static AppIcon HomeIcon => new() { Kind = AppIconKind.Home, Size = 24, Foreground = Brushes.White };
    public static AppIcon SettingsIcon => new() { Kind = AppIconKind.Settings, Size = 24, Foreground = Brushes.White };
    public static AppIcon BellIcon => new() { Kind = AppIconKind.Bell, Size = 24, Foreground = Brushes.White };
    public static AppIcon SearchIcon => new() { Kind = AppIconKind.Search, Size = 24, Foreground = Brushes.White };
    public static AppIcon UserIcon => new() { Kind = AppIconKind.Users, Size = 24, Foreground = Brushes.White };
}