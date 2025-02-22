using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules;

namespace Round.NET.AvaloniaApp.MQTTMangeView.Views.Pages.Main;

public partial class Log : UserControl
{
    public Log()
    {
        InitializeComponent();
        Core.LogPage = this;
    }
}