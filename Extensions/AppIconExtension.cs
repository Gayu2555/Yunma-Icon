// Extensions/AppIconExtension.cs
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace YunmaIcons.Avalonia;

public class AppIconExtension : MarkupExtension
{
    public AppIconKind Kind { get; set; }
    public double Size { get; set; } = 24;
    public IBrush? Foreground { get; set; }
    public double StrokeWidth { get; set; } = 2;
    public AppIconAnimation Animate { get; set; } = AppIconAnimation.None;

    // Constructor biar bisa pakai {yi:AppIcon Home}
    public AppIconExtension() { }
    public AppIconExtension(AppIconKind kind) => Kind = kind;
    public AppIconExtension(AppIconKind kind, double size)
    {
        Kind = kind;
        Size = size;
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return new AppIcon
        {
            Kind = Kind,
            Size = Size,
            StrokeWidth = StrokeWidth,
            Animate = Animate,
            Foreground = Foreground
        };
    }
}