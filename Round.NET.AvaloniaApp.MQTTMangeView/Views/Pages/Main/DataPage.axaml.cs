using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;
using FluentAvalonia.UI.Controls;
using HeroIconsAvalonia.Controls;
using HeroIconsAvalonia.Enums;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules.Entry;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules.Project;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules.View;

namespace Round.NET.AvaloniaApp.MQTTMangeView.Views.Pages.Main;

public partial class DataPage : UserControl
{
    public DataPage()
    {
        InitializeComponent();
        Task.Run(() =>
        {
            var num = 0;
            while (true)
            {
                if (num != Project.NowProject.DataItems.Count)
                {
                    num = Project.NowProject.DataItems.Count;
                    Dispatcher.UIThread.Invoke(UpdateTopicList);
                }
                Thread.Sleep(100);
            }
        });
    }

    private void UpdateTopicList()
    {
        DataListBox.Children.Clear();
        DataMange.DataItems.Clear();
        foreach (var topic in Project.NowProject.DataItems)
        {
            var showlabel = new Label()
            {
                Content = "NULL",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(10),
                FontSize = 20,
                FontWeight = FontWeight.Bold
            };
            var conf = new DataItemEntry()
            {
                Topic = topic.Topic,
                Remark = topic.Remark,
                Label = showlabel,
            };
            DataMange.DataItems.Add(conf);
            #region DelButton

            var delbtn = new Button()
            {
                Content = new HeroIcon()
                {
                    Foreground = new SolidColorBrush(Colors.White),
                    Type = IconType.Trash,
                    Min = true,
                },
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(5),
                Width = 32,
                Height = 32,
                Background = new SolidColorBrush(Colors.Transparent),
                BorderBrush = new SolidColorBrush(Colors.Transparent),
                BorderThickness = new Thickness(0)
            };
            delbtn.Click += (sender, args) =>
            {
                var con = new ContentDialog()
                {
                    Title = $"删除内容项 {topic.Topic}",
                    Content = $"你确定要删除 {topic.Topic} 的监听项 ?\n删除后将无法找回！",
                    PrimaryButtonText = "取消",
                    CloseButtonText = "确定",
                    DefaultButton = ContentDialogButton.Close
                };
                con.CloseButtonClick += (sender, args) => { DataMange.DeleteDataItem(topic); };
                con.ShowAsync();
            };

            #endregion

            #region CopyButton

            var copybtn = new Button()
            {
                Content = new HeroIcon()
                {
                    Foreground = new SolidColorBrush(Colors.White),
                    Type = IconType.Clipboard,
                    Min = true,
                },
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(5),
                Width = 32,
                Height = 32,
                Background = new SolidColorBrush(Colors.Transparent),
                BorderBrush = new SolidColorBrush(Colors.Transparent),
                BorderThickness = new Thickness(0)
            };
            copybtn.Click += async (sender, args) =>
            {
                await Core.AppWindow.Clipboard.SetTextAsync(showlabel.Content.ToString());
                Core.AppWindow.ShowMessage($"已复制 {topic.Topic} 的内容！", "主题管理", NotificationType.Success);
            };

            #endregion
            var topicItem = new ComboBoxItem()
            {
                Content = new Grid()
                {
                    Children =
                    {
                        showlabel,
                        new Label()
                        {
                            Content = $"主题：{topic.Topic.Split('/')[1]} 在 {topic.Topic.Split('/')[0]} 下。备注：{topic.Remark}",
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Bottom,
                            Margin = new Thickness(10),
                            Foreground = new SolidColorBrush(Colors.Gray),
                        },
                        new DockPanel()
                        {
                            Children =
                            {
                                copybtn,
                                delbtn
                            },
                            HorizontalAlignment = HorizontalAlignment.Right,
                            VerticalAlignment = VerticalAlignment.Center,
                        }
                    },
                    Height = 80
                },
                Height = 80,
                Margin = new Thickness(0, 5),
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
            };

            DataListBox.Children.Add(topicItem);
        }
    }

    private void AddDataItem_OnClick(object? sender, RoutedEventArgs e)
    {
        var add = new AddData.AddData();
        var con = new ContentDialog()
        {
            Title = "添加数据监视",
            Content = add,
            PrimaryButtonText = "取消",
            CloseButtonText = "添加",
            DefaultButton = ContentDialogButton.Close
        };
        con.CloseButtonClick += (_, __) =>
        {
            DataMange.AddDataItem(new DataEntry()
            {
                Topic = Project.NowProject.Topics[add.TopicSelBox.SelectedIndex].Topic,
                Remark = add.NoteBox.Text
            });
        };
        con.ShowAsync();
    }
}