using System;
using System.Collections.Generic;
using Avalonia.Controls;

namespace Round.NET.AvaloniaApp.MQTTMangeView.Modules.Enum;

public static class ControlTypeEnumExtensions
{
    public static List<ControlTypeClass> ControlTypes { get; set; } = new();
    public class ControlTypeClass
    {
        public enum ControlTypeEnum
        {
            Button,
            Label,
            Image
        }
        public string Key { get; set; } = string.Empty;
        public ControlTypeEnum ControlType { get; set; }
        public UserControl AddPage { get; set; }
        public Action<UserControl> AddActon { get; set; }
    }
} 