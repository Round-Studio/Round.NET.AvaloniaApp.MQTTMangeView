using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using FluentAvalonia.UI.Windowing;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules.Project;

namespace Round.NET.AvaloniaApp.MQTTMangeView.Views;

public partial class MainWindow : AppWindow
{
    public WindowNotificationManager? _manager;
    public MainWindow(string[] args)
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
        
        Core.AppWindow = this;
        _manager = new WindowNotificationManager(this)
        {
            MaxItems = 2
        };
        _manager.Position = NotificationPosition.BottomRight;
        this.SizeChanged += (s, e) =>
        {
            Project.NowProject.WinWidth = (int)this.Bounds.Width;
            Project.NowProject.WinHeight = (int)this.Bounds.Height;
        };
        Core.Initialize();
        if (args.Length > 0)
        {
            Project.OpenProject(args[0]);
        }
    }
    public void ShowMessage(string message, string title, NotificationType type = NotificationType.Information)
    {
        _manager.Show(new Notification(title, message, type));
    }
}