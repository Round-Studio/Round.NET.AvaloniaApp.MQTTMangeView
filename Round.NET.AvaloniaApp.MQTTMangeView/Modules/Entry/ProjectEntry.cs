using System.Collections.Generic;

namespace Round.NET.AvaloniaApp.MQTTMangeView.Modules.Entry;

public class ProjectEntry
{
    public string Title { get; set; } = string.Empty;
    public int Port { get; set; } = 1883;
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public bool EditMode { get; set; } = true;
    public int WinWidth { get; set; } = 800;
    public int WinHeight { get; set; } = 600;
    public List<TopicEntry> Topics { get; set; } = new();
    public List<ControlEntry> Controls { get; set; } = new();
    public List<DataEntry> DataItems { get; set; } = new();
}