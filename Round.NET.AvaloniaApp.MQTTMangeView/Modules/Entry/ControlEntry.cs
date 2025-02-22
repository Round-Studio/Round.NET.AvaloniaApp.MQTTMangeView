using System;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules.Enum;

namespace Round.NET.AvaloniaApp.MQTTMangeView.Modules.Entry;

public class ControlEntry
{
    public ControlTypeEnumExtensions.ControlTypeClass.ControlTypeEnum Type { get; set; }
    public decimal? Width { get; set; } = 120;
    public decimal? Height { get; set; } = 50;
    public string Topic { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string GUID { get; set; } = Guid.NewGuid().ToString();
    public int X { get; set; } = 0;
    public int Y { get; set; } = 0;
}