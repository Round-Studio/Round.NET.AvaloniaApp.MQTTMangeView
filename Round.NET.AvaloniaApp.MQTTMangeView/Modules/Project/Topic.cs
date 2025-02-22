using Round.NET.AvaloniaApp.MQTTMangeView.Modules.Entry;

namespace Round.NET.AvaloniaApp.MQTTMangeView.Modules.Project;

public class Topic
{
    public static bool AddTopic(TopicEntry topic)
    {
        foreach (var qtopic in Project.NowProject.Topics)
        {
            if (qtopic.Topic == topic.Topic)
            {
                return false;
            }
        }
        Project.NowProject.Topics.Add(topic);
        return true;
    }

    public static int GetTopicCount()
    {
        return Project.NowProject.Topics.Count;
    }

    public static void DeleteTopic(TopicEntry topic)
    {
        Project.NowProject.Topics.Remove(topic);
    }
}