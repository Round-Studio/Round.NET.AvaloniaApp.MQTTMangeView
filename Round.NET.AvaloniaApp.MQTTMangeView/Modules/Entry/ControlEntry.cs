using Round.NET.AvaloniaApp.MQTTMangeView.Modules.Enum;

namespace Round.NET.AvaloniaApp.MQTTMangeView.Modules.Entry;

public class ControlEntry
{
    public ControlTypeEnumExtensions.ControlTypeClass.ControlTypeEnum Type { get; set; }
    public decimal? Width { get; set; } = 120;
    public decimal? Height { get; set; } = 50;
    public string Topic { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}