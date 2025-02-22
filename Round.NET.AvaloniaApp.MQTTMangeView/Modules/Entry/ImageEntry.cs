using System;
using Avalonia.Controls;

namespace Round.NET.AvaloniaApp.MQTTMangeView.Modules.Entry;

public class ImageEntry
{
    public Image Image { get; set; } = null!;
    public string Topic { get; set; } = String.Empty;
}