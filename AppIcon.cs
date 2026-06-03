using System;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.VisualTree;

// Explicit alias biar ga ambiguous
using Path = Avalonia.Controls.Shapes.Path;
using TextElement = Avalonia.Controls.Primitives.TemplatedControl;
namespace YunmaIcons.Avalonia;

public class AppIcon : Viewbox
{
    public static readonly StyledProperty<double> SizeProperty =
        AvaloniaProperty.Register<AppIcon, double>(nameof(Size), 24);

    public double Size
    {
        get => GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    public static readonly StyledProperty<AppIconKind> KindProperty =
        AvaloniaProperty.Register<AppIcon, AppIconKind>(nameof(Kind));

    public AppIconKind Kind
    {
        get => GetValue(KindProperty);
        set => SetValue(KindProperty, value);
    }

    public static readonly StyledProperty<double> StrokeWidthProperty =
        AvaloniaProperty.Register<AppIcon, double>(nameof(StrokeWidth), 2.0);

    public double StrokeWidth
    {
        get => GetValue(StrokeWidthProperty);
        set => SetValue(StrokeWidthProperty, value);
    }

    public static readonly StyledProperty<AppIconAnimation> AnimateProperty =
        AvaloniaProperty.Register<AppIcon, AppIconAnimation>(nameof(Animate), AppIconAnimation.None);

    public AppIconAnimation Animate
    {
        get => GetValue(AnimateProperty);
        set => SetValue(AnimateProperty, value);
    }

    public static readonly StyledProperty<IBrush?> ForegroundProperty =
        AvaloniaProperty.Register<AppIcon, IBrush?>(nameof(Foreground), defaultValue: null);

    public IBrush? Foreground
    {
        get => GetValue(ForegroundProperty);
        set => SetValue(ForegroundProperty, value);
    }

    private readonly Canvas _canvas = new();
    private CancellationTokenSource? _animCts;

    public AppIcon()
    {
        _canvas.Width = 24;
        _canvas.Height = 24;
        _canvas.Background = Brushes.Transparent;
        Child = _canvas;
        Width = Size;
        Height = Size;
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        UpdateIcon();
        ApplyAnimation();
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        _animCts?.Cancel();
        _animCts = null;
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == SizeProperty)
        {
            Width = Size;
            Height = Size;
        }
        else if (change.Property == KindProperty)
            UpdateIcon();
        else if (change.Property == StrokeWidthProperty)
            UpdateStrokes();
        else if (change.Property == AnimateProperty)
            ApplyAnimation();
        else if (change.Property == ForegroundProperty)
            UpdateStrokes();
    }

    private IBrush ResolveBrush()
    {
        if (Foreground != null)
            return Foreground;

        // Walk up visual tree
        var parent = this.GetVisualParent();
        while (parent != null)
        {
            if (parent is Control ctrl)
            {
                var brush = ctrl.GetValue(TextElement.ForegroundProperty);
                if (brush != null && brush != Brushes.Black)
                    return brush;
            }
            parent = parent.GetVisualParent();
        }

        return Brushes.White;
    }

    private void UpdateIcon()
    {
        _canvas.Children.Clear();
        if (!AppIconRegistry.Icons.TryGetValue(Kind, out var paths)) return;

        var brush = ResolveBrush();
        foreach (var d in paths)
        {
            _canvas.Children.Add(new Path
            {
                Data = Geometry.Parse(d),
                Stroke = brush,
                StrokeThickness = StrokeWidth,
                StrokeLineCap = PenLineCap.Round,
                StrokeJoin = PenLineJoin.Round,
                Fill = Brushes.Transparent
            });
        }
    }

    private void UpdateStrokes()
    {
        var brush = ResolveBrush();
        foreach (var child in _canvas.Children)
        {
            if (child is Path path)
            {
                path.Stroke = brush;
                path.StrokeThickness = StrokeWidth;
            }
        }
    }

    private void ApplyAnimation()
    {
        _animCts?.Cancel();
        _animCts = null;

        RenderTransform = null;
        RenderTransformOrigin = RelativePoint.Center;

        if (Animate == AppIconAnimation.None) return;

        _animCts = new CancellationTokenSource();
        var token = _animCts.Token;

        switch (Animate)
        {
            case AppIconAnimation.Spin:   RunSpin(token);   break;
            case AppIconAnimation.Pulse:  RunPulse(token);  break;
            case AppIconAnimation.Bounce: RunBounce(token); break;
        }
    }

    private async void RunSpin(CancellationToken ct)
    {
        var rotate = new RotateTransform(0);
        RenderTransform = rotate;
        RenderTransformOrigin = RelativePoint.Center;

        var anim = new Animation
        {
            Duration = TimeSpan.FromSeconds(1),
            IterationCount = IterationCount.Infinite,
            Easing = new LinearEasing(),
            Children =
            {
                new KeyFrame { Cue = new Cue(0d), Setters = { new Setter(RotateTransform.AngleProperty, 0d) } },
                new KeyFrame { Cue = new Cue(1d), Setters = { new Setter(RotateTransform.AngleProperty, 360d) } }
            }
        };

        try { await anim.RunAsync(this, ct); } catch { }
    }

    private async void RunPulse(CancellationToken ct)
    {
        var scale = new ScaleTransform(1, 1);
        RenderTransform = scale;
        RenderTransformOrigin = RelativePoint.Center;

        var anim = new Animation
        {
            Duration = TimeSpan.FromSeconds(1),
            IterationCount = IterationCount.Infinite,
            Easing = new SineEaseInOut(),
            Children =
            {
                new KeyFrame { Cue = new Cue(0d), Setters = { new Setter(ScaleTransform.ScaleXProperty, 1d), new Setter(ScaleTransform.ScaleYProperty, 1d) } },
                new KeyFrame { Cue = new Cue(0.5d), Setters = { new Setter(ScaleTransform.ScaleXProperty, 1.3d), new Setter(ScaleTransform.ScaleYProperty, 1.3d) } },
                new KeyFrame { Cue = new Cue(1d), Setters = { new Setter(ScaleTransform.ScaleXProperty, 1d), new Setter(ScaleTransform.ScaleYProperty, 1d) } }
            }
        };

        try { await anim.RunAsync(this, ct); } catch { }
    }

    private async void RunBounce(CancellationToken ct)
    {
        var translate = new TranslateTransform(0, 0);
        RenderTransform = translate;
        RenderTransformOrigin = RelativePoint.Center;

        var anim = new Animation
        {
            Duration = TimeSpan.FromSeconds(0.6),
            IterationCount = IterationCount.Infinite,
            Easing = new SineEaseInOut(),
            Children =
            {
                new KeyFrame { Cue = new Cue(0d), Setters = { new Setter(TranslateTransform.YProperty, 0d) } },
                new KeyFrame { Cue = new Cue(0.5d), Setters = { new Setter(TranslateTransform.YProperty, -6d) } },
                new KeyFrame { Cue = new Cue(1d), Setters = { new Setter(TranslateTransform.YProperty, 0d) } }
            }
        };

        try { await anim.RunAsync(this, ct); } catch { }
    }
}