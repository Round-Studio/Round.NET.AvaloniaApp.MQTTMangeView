using System.Collections.Generic;

namespace Round.NET.AvaloniaApp.MQTTMangeView.Modules.Entry;

public class ProjectEntry
{
    public string Title { get; set; } = string.Empty;
    public int Port = 1883;
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public List<TopicEntry> Topics { get; set; } = new();
    public List<ControlEntry> Controls { get; set; } = new();
}