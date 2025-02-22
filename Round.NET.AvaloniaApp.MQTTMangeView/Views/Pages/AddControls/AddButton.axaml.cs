using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules.Project;

namespace Round.NET.AvaloniaApp.MQTTMangeView.Views.Pages.AddControls;

public partial class AddButton : UserControl
{
    public AddButton()
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