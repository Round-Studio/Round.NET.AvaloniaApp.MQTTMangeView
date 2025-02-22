using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules.Entry;

namespace Round.NET.AvaloniaApp.MQTTMangeView.Modules.View;

public class DataMange
{
    public static List<DataItemEntry> DataItems { get; set; } = new();
    public static void LaunchUpdateDatas()
    {
        Task.Run(() =>
        {
            var ima = new MessageEntry();
            while (true)
            {
                if (ima.Message != Core.NowMessage.Message || ima.Topic != Core.NowMessage.Topic)
                {
                    foreach (var data in DataItems)
                    {
                        if (data.Topic == Core.NowMessage.Topic)
                        {
                            if (data.Label != null)
                            {
                                var truncatedMessage = "";
                                if (Core.NowMessage.Message.Length >= 220)
                                {
                                    truncatedMessage = Core.NowMessage.Message.Substring(0, 80) + "(此处省略...)" +
                                                           Core.NowMessage.Message.Substring(
                                                               Core.NowMessage.Message.Length - 80);
                                }
                                else
                                {
                                    truncatedMessage = Core.NowMessage.Message;
                                }
                                Dispatcher.UIThread.InvokeAsync(() => data.Label.Content = truncatedMessage);
                            }   
                        }
                    }
                    ima = Core.NowMessage;
                }
            }
        });
    }

    public static void AddDataItem(DataEntry entry)
    {
        Project.Project.NowProject.DataItems.Add(entry);
    }

    public static void DeleteDataItem(DataEntry entry)
    {
        Project.Project.NowProject.DataItems.Remove(entry);
    }
}