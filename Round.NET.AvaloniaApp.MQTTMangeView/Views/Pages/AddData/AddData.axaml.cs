using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules.Project;

namespace Round.NET.AvaloniaApp.MQTTMangeView.Views.Pages.AddData;

public partial class AddData : UserControl
{
    public AddData()
    {
        InitializeComponent();
        if (Project.NowProject.Topics.Count == 0)
        {
            TopicSelBox.Items.Add(new ComboBoxItem()
            {
                Content = "(主题为空)"
            });
            
            TopicSelBox.SelectedIndex = 0;
            TopicSelBox.IsEnabled = false;
        }
        else
        {
            Project.NowProject.Topics.ForEach((entry =>
            {
                TopicSelBox.Items.Add(new ComboBoxItem()
                {
                    Content = entry.Topic
                });
            }));
            TopicSelBox.SelectedIndex = 0;
        }
    }
}