using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules.Entry;

namespace Round.NET.AvaloniaApp.MQTTMangeView.Modules.Project;

public class Project
{
    public static ProjectEntry NowProject { get; set; } = new();

    public static void OpenProject(string projectName)
    {
        NowProject = JsonSerializer.Deserialize<ProjectEntry>(File.ReadAllText(projectName));
    }

    public static void SaveProject()
    {
        
    }

    public static string GetProject()
    {
        string result = Regex.Unescape(JsonSerializer.Serialize(NowProject, new JsonSerializerOptions() { WriteIndented = true })); //获取结果并转换成正确的格式
            
        return result;
    }
}