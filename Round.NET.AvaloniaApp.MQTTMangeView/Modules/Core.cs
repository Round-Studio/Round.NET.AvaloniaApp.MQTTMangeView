using System;
using Avalonia.Controls;
using Avalonia.Layout;
using FluentAvalonia.UI.Windowing;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules.Entry;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules.Enum;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules.View;
using Round.NET.AvaloniaApp.MQTTMangeView.Views.Pages.AddControls;

namespace Round.NET.AvaloniaApp.MQTTMangeView.Modules;

public class Core
{
    public static AppWindow AppWindow { get; set; } = null;
    public static Grid MainGrid { get; set; } = null;

    public static void Initialize()
    {
        #region 注册控件

        ControlTypeEnumExtensions.ControlTypes.Add(new()
        {
            Key = "Button",
            ControlType = ControlTypeEnumExtensions.ControlTypeClass.ControlTypeEnum.Button,
            AddPage = new AddButton(),
            AddActon = (ob) =>
            {
                var us = (AddButton)ob;
                if (us.TopicSelBox.IsEnabled)
                {
                    Project.Project.NowProject.Controls.Add(new ControlEntry()
                    {
                        Topic = Project.Project.NowProject.Topics[us.TopicSelBox.SelectedIndex].Topic,
                        Content = us.ShowText.Text,
                        Width = us.WidthBox.Value,
                        Height = us.HeightBox.Value,
                        Type = ControlTypeEnumExtensions.ControlTypeClass.ControlTypeEnum.Button
                    });
                }
            }
        });
        ControlTypeEnumExtensions.ControlTypes.Add(new()
        {
            Key = "Image",
            ControlType = ControlTypeEnumExtensions.ControlTypeClass.ControlTypeEnum.Image,
            AddPage = new AddImage(),
            AddActon = (ob) =>
            {
                
            }
        });
        ControlTypeEnumExtensions.ControlTypes.Add(new()
        {
            Key = "Label",
            ControlType = ControlTypeEnumExtensions.ControlTypeClass.ControlTypeEnum.Label,
            AddPage = new AddLabel(),
            AddActon = (ob) =>
            {
                
            }
        });

        #endregion
    }
}