using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using FluentAvalonia.UI.Windowing;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules;

namespace Round.NET.AvaloniaApp.MQTTMangeView.Views;

public partial class MainWindow : AppWindow
{
    public MainWindow()
    {
        InitializeComponent();
        TitleBar.ExtendsContentIntoTitleBar = true;
        TitleBar.TitleBarHitTestType = TitleBarHitTestType.Complex;
        TitleBar.Height = 40;
        RenderOptions.SetTextRenderingMode(this, TextRenderingMode.SubpixelAntialias); // 字体渲染模式
        RenderOptions.SetBitmapInterpolationMode(this, BitmapInterpolationMode.MediumQuality); // 图片渲染模式
        RenderOptions.SetEdgeMode(this, EdgeMode.Antialias); // 形状渲染模式
        this.Background = Brushes.Transparent;
        this.TransparencyLevelHint = new[] { WindowTransparencyLevel.Mica };
        Core.Initialize();
        
        Core.AppWindow = this;
    }
}