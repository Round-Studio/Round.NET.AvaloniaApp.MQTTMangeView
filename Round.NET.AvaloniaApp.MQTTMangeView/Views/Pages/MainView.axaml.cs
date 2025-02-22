using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using FluentAvalonia.UI.Controls;
using FluentAvalonia.UI.Windowing;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules.Project;
using Round.NET.AvaloniaApp.MQTTMangeView.Views.Pages.Main;

namespace Round.NET.AvaloniaApp.MQTTMangeView.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }
    public Mange Mange = new();
    public Setting Setting = new();
    public ViewPage ViewPage = new();
    public Log Log = new();
    private void NavigationView_OnSelectionChanged(object? sender, NavigationViewSelectionChangedEventArgs e)
    {
        var nav = sender as NavigationView;
        var tag = (nav.SelectedItem as NavigationViewItem)?.Tag as string;

        switch (tag)
        {
            case "Mange":
                nav.Content = Mange;
                break;
            case "Setting":
                nav.Content = Setting;
                break;
            case "View":
                nav.Content = ViewPage;
                break;
            case "Log":
                nav.Content = Log;
                break;
        }
    }

    private async void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        var saveFileDialog = new SaveFileDialog
        {
            Title = "保存项目文件",
            InitialFileName = $"My MQTT View Project",
            DefaultExtension = "rmqvm",
            Filters = new List<FileDialogFilter>
            {
                new FileDialogFilter { Name = "Round MQTT View Mange 项目文件", Extensions = new List<string> { "rmqvm" } }
            }
        };

        string? filePath = await saveFileDialog.ShowAsync(Core.AppWindow);

        if (!string.IsNullOrEmpty(filePath))
        {
            File.WriteAllText(filePath, Project.GetProject());
        }
    }

    private async void Open_OnClick(object? sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog
        {
            Title = "打开项目",
            AllowMultiple = false,
            Filters = new List<FileDialogFilter>
            {
                new FileDialogFilter { Name = "Round MQTT View Mange 项目文件", Extensions = new List<string> { "rmqvm" } }
            }
        };

        // ShowAsync returns an array of selected file paths
        string[]? filePaths = await openFileDialog.ShowAsync(Core.AppWindow);
        if (File.Exists(filePaths[0]))
        {
            if (filePaths != null)
            {
                Project.OpenProject(filePaths[0]);
            }
        }
    }
}