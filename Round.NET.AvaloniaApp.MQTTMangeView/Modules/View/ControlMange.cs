using System;
using System.Drawing;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;
using FluentAvalonia.UI.Controls;
using MQTTnet.Server;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules.Entry;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules.Enum;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules.Server;
using Point = Avalonia.Point;

namespace Round.NET.AvaloniaApp.MQTTMangeView.Modules.View;

public class ControlMange
{
    public static void AddControl(string type)
    {
        GetControl(type);
    }

    public static void EditControl(ControlEntry entry)
    {
        // Console.WriteLine($"编辑模式 {entry.GUID}");
        
        
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
                    VerticalAlignment = VerticalAlignment.Top,
                };
                break;
            case ControlTypeEnumExtensions.ControlTypeClass.ControlTypeEnum.Image:
                var image = new Image()
                {
                    Width = (double)entry.Width,
                    Height = (double)entry.Height,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                };
                con = new Grid()
                {
                    Background = Brushes.DarkGray,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Children = { image }
                };
                ImageMange.Images.Add(new ()
                {
                    Topic = entry.Topic,
                    Image = image
                });
                break;
        }
        bool _isDragging = false; 
        Point _startPoint = default;
        con.PointerPressed += (s, e) =>
        {
            if (e.Pointer.Type == PointerType.Mouse)
            {
                var currentPoint = e.GetCurrentPoint(Core.AppWindow);

                if (currentPoint.Properties.IsRightButtonPressed && Project.Project.NowProject.EditMode)
                {
                    // 右键按下，进入拖拽模式
                    _isDragging = true;
                    _startPoint = e.GetPosition(Core.AppWindow); // 获取鼠标按下时的位置
                }
                else if (currentPoint.Properties.IsLeftButtonPressed && Project.Project.NowProject.EditMode)
                {
                    EditControl(entry);
                }
            }
        };
        if (con.GetType() == typeof(Button))
        {
            var button = (Button)con;
            button.Click += (s, e) =>
            {
                if (Project.Project.NowProject.EditMode)
                {
                    EditControl(entry);
                }
                else
                {
                    MQTTServer.Sendmesage(entry.Topic, entry.Body);
                }
            };
        }
        con.PointerReleased += (s, e) =>
        {
            _isDragging = false;

            foreach (var ent in Project.Project.NowProject.Controls)
            {
                if (ent.GUID == entry.GUID)
                {
                    var con = s as Control;
                    if (con != null)
                    {
                        var transform = con.RenderTransform as TranslateTransform;
                        if (transform != null)
                        {
                            ent.X = (int)transform.X;
                            ent.Y = (int)transform.Y;
                        }
                        else
                        {
                            // 如果没有 RenderTransform，可能需要设置默认值
                            ent.X = 0;
                            ent.Y = 0;
                        }
                    }
                }
            }
        };
        con.PointerMoved += (s, e) =>
        {
            if (_isDragging)
            {
                var currentPoint = e.GetPosition(Core.AppWindow); // 获取当前鼠标位置
                var deltaX = currentPoint.X - _startPoint.X; // 计算水平偏移量
                var deltaY = currentPoint.Y - _startPoint.Y; // 计算垂直偏移量

                // 更新控件的位置
                var border = s as Control;
                var transform = border?.RenderTransform as TranslateTransform ?? new TranslateTransform();
                transform.X += deltaX;
                transform.Y += deltaY;

                border.RenderTransform = transform;

                // 更新起点位置
                _startPoint = currentPoint;
            }
        };
        con.RenderTransform = new TranslateTransform()
        {
            X = entry.X,
            Y = entry.Y
        };
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