using System;
using Avalonia.Controls;
using Avalonia.Layout;
using FluentAvalonia.UI.Controls;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules.Entry;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules.Enum;

namespace Round.NET.AvaloniaApp.MQTTMangeView.Modules.View;

public class ControlMange
{
    public static void AddControl(string type)
    {
        GetControl(type);
    }

    public static void AddShowControl(ControlEntry entry)
    {
        var con = new Control();
        switch (entry.Type)
        {
            case ControlTypeEnumExtensions.ControlTypeClass.ControlTypeEnum.Button:
                con = new Button()
                {
                    Content = entry.Content,
                    Width = (double)entry.Width,
                    Height = (double)entry.Height,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top
                };
                break;
        }
        Core.MainGrid.Children.Add(con);
    }
    public static ControlEntry GetControl(string type)
    {
        var entry = ControlTypeEnumExtensions.ControlTypes.Find((e => e.Key == type));
        Type pageType = entry.AddPage.GetType();
        var PageInstance = Activator.CreateInstance(pageType);
        
        var control = new ControlEntry()
        {
            Type = entry.ControlType
        };
        var con = new ContentDialog()
        {
            Title = $"添加 {entry.Key}",
            Content = PageInstance,
            PrimaryButtonText = "取消",
            CloseButtonText = "添加",
            DefaultButton = ContentDialogButton.Close
        };
        con.CloseButtonClick += (_, __) =>
        {
            entry.AddActon((UserControl)PageInstance);
        };
        con.ShowAsync();
        
        return control;
    }
}