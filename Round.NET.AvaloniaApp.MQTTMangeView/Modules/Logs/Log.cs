using System;
using Avalonia.Controls;
using Avalonia.Threading;

namespace Round.NET.AvaloniaApp.MQTTMangeView.Modules.Logs;

public class Log
{
    public static void WriteLine(dynamic message)
    {
        var date = DateTime.Now.ToString("[HH:mm:ss]");
        // Console.WriteLine($"{date}: {message}");
        
        string truncatedMessage = message;

        // 如果消息长度超过100个字符
        if (message.Length > 100)
        {
            // 截取前20个字符和后20个字符，中间用省略号替代
            truncatedMessage = message.Substring(0, 80) + "(此处省略...)" + message.Substring(message.Length - 80);
        }
        Dispatcher.UIThread.InvokeAsync(() =>
        {
            Core.LogPage.LogPanel.Children.Add(new Label()
            {
                Content = $"{date}: {truncatedMessage}",
            });

            ((ScrollViewer)Core.LogPage.LogPanel.Parent).ScrollToEnd();
        });
    }
}