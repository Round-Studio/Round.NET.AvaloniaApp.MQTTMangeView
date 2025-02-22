using System;
using Avalonia.Controls;
using Avalonia.Layout;
using FluentAvalonia.UI.Windowing;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules.Entry;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules.Enum;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules.View;
using Round.NET.AvaloniaApp.MQTTMangeView.Views;
using Round.NET.AvaloniaApp.MQTTMangeView.Views.Pages.AddControls;
using Round.NET.AvaloniaApp.MQTTMangeView.Views.Pages.Main;

namespace Round.NET.AvaloniaApp.MQTTMangeView.Modules;

public class Core
{
    public static MainWindow AppWindow { get; set; } = null;
    public static Grid MainGrid { get; set; } = null;
    public static MessageEntry NowMessage { get; set; } = new();
    public static Log LogPage { get; set; } = new();
    public static bool IsInitialized { get; private set; } = false;
    public static void Initialize()
    {
        ImageMange.LaunchUpdateImages();
        DataMange.LaunchUpdateDatas();
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
                        Type = ControlTypeEnumExtensions.ControlTypeClass.ControlTypeEnum.Button,
                        Body = us.ClickMessage.Text
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
                var us = (AddImage)ob;
                if (us.TopicSelBox.IsEnabled)
                {
                    Project.Project.NowProject.Controls.Add(new ControlEntry()
                    {
                        Topic = Project.Project.NowProject.Topics[us.TopicSelBox.SelectedIndex].Topic,
                        Width = us.WidthBox.Value,
                        Height = us.HeightBox.Value,
                        Type = ControlTypeEnumExtensions.ControlTypeClass.ControlTypeEnum.Image
                    });
                }
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
        IsInitialized = true;
    }
}