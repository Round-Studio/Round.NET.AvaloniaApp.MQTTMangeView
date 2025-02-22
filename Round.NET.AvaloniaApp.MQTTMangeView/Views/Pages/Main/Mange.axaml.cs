using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;
using FluentAvalonia.UI.Controls;
using HeroIconsAvalonia.Controls;
using HeroIconsAvalonia.Enums;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules.Entry;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules.Project;

namespace Round.NET.AvaloniaApp.MQTTMangeView.Views.Pages.Main;

public partial class Mange : UserControl
{
    public Mange()
    {
        InitializeComponent();
        Task.Run(() =>
        {
            var con = 0;
            while (true)
            {
                if (Topic.GetTopicCount() != con)
                {
                    Dispatcher.UIThread.Invoke(UpdateTopicList);
                    con = Topic.GetTopicCount();
                }
                Thread.Sleep(100);
            }
        });
    }

    private void AddTopic_OnClick(object? sender, RoutedEventArgs e)
    {
        var con = new ContentDialog
        {
            Title = "添加 Topic",
            CloseButtonText = "确定",
            DefaultButton = ContentDialogButton.Close
        };
        var topichead = TopicHead.Text;
        var topicbody = TopicBody.Text;
        if (string.IsNullOrEmpty(topichead) || string.IsNullOrEmpty(topicbody))
        {
            con.Content = "无效值";
            con.ShowAsync();
            return;
        }

        var adtopic = $"{topichead}/{topicbody}";
        var topic = new TopicEntry()
        {
            Topic = adtopic
        };
        if (Topic.AddTopic(topic))
        {
            con.Content = $"{adtopic} 添加成功";
            con.ShowAsync();
            TopicBody.Text = string.Empty;
        }
        else
        {
            con.Content = $"{adtopic} 添加失败";
            con.ShowAsync();
        }
    } // 添加主题

    private void UpdateTopicList()
    {
        TopicListBox.Children.Clear();
        foreach (var topic in Project.NowProject.Topics)
        {
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
                Margin = new Thickness(10),
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
                    Title = $"删除主题 {topic.Topic}",
                    Content = $"你确定要删除 {topic.Topic} ?\n删除后将无法找回！",
                    PrimaryButtonText = "取消",
                    CloseButtonText = "确定",
                    DefaultButton = ContentDialogButton.Close
                };
                con.CloseButtonClick += (sender, args) =>
                {
                    Topic.DeleteTopic(topic);
                };
                con.ShowAsync();
            };
            var topicItem = new ComboBoxItem()
            {
                Content = new Grid()
                {
                    Children =
                    {
                        new Label()
                        {
                            Content = topic.Topic,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Top,
                            Margin = new Thickness(10),
                            FontSize = 20,
                            FontWeight = FontWeight.Bold
                        },
                        new Label()
                        {
                            Content = $"主题：{topic.Topic.Split('/')[1]} 在 {topic.Topic.Split('/')[0]} 下",
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Bottom,
                            Margin = new Thickness(10),
                            Foreground = new SolidColorBrush(Colors.Gray),
                        },
                        delbtn
                    },
                    Height = 80
                },
                Height = 80,
                Margin = new Thickness(0,5),
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
            };
            
            TopicListBox.Children.Add(topicItem);
        }
    }
}