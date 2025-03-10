﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules.Project;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules.View;

namespace Round.NET.AvaloniaApp.MQTTMangeView.Views.Pages.Main;

public partial class ViewPage : UserControl
{
    public ViewPage()
    {
        InitializeComponent();
        Core.MainGrid = this.MainView;
        Task.Run(() =>
        {
            var con = 0;
            while (true)
            {
                Dispatcher.UIThread.Invoke(() =>
                {
                    if (EditModel.IsChecked != Project.NowProject.EditMode)
                    {
                        EditModel.IsChecked = Project.NowProject.EditMode;
                    }
                });
                if (con != Project.NowProject.Controls.Count)
                {
                    con = Project.NowProject.Controls.Count;
                    ImageMange.Images.Clear();

                    Dispatcher.UIThread.Invoke(() =>
                    {
                        MainView.Children.Clear();
                        Project.NowProject.Controls.ForEach((e) =>
                        {
                            ControlMange.AddShowControl(e);
                        });
                    });
                }
                Thread.Sleep(100);
            }
        });
    }

    private void AddControl_OnClick(object? sender, RoutedEventArgs e)
    {
        var tag = (sender as Control)?.Tag as string;
        ControlMange.AddControl(tag);
    }

    private void EditModel_OnClick(object? sender, RoutedEventArgs e)
    {
        var ob = (sender as CheckBox);
        Project.NowProject.EditMode = (bool)ob.IsChecked;
    }
}