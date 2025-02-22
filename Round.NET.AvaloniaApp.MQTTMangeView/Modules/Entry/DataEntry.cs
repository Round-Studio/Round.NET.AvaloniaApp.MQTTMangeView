using System;
using Avalonia.Controls;

namespace Round.NET.AvaloniaApp.MQTTMangeView.Modules.Entry;

public class DataEntry
{
    public string Topic { get; set; } = string.Empty;
    public string Remark { get; set; } = "暂无介绍...";
}

public class DataItemEntry : DataEntry
{
    public Label Label { get; set; } = new();
    public string DUID { get; set; } = Guid.NewGuid().ToString();
}