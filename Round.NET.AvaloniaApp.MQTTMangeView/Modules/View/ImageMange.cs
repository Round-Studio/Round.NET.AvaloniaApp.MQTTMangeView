using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules.Entry;

namespace Round.NET.AvaloniaApp.MQTTMangeView.Modules.View;

public class ImageMange
{
    public static List<ImageEntry> Images { get; set; } = new();

    public static void LaunchUpdateImages()
    {
        Task.Run(() =>
        {
            var ima = new MessageEntry();
            while (true)
            {
                if (ima.Message != Core.NowMessage.Message || ima.Topic != Core.NowMessage.Topic)
                {
                    foreach (var image in Images)
                    {
                        if (image.Topic == ima.Topic)
                        {
                            if (image != null)
                            {
                                // 获取Base64字符串
                                var base64String = Core.NowMessage.Message.Replace("data:image/png;base64,","");
                                // Console.WriteLine($"Image: {base64String.Substring(0,50)}");
                                // 将Base64字符串转换为图片
                                if (!string.IsNullOrEmpty(base64String))
                                {
                                    try
                                    {
                                        // 将Base64字符串转换为字节数组
                                        byte[] imageBytes = Convert.FromBase64String(base64String);

                                        // 在主线程上更新UI
                                        Dispatcher.UIThread.Invoke(() =>
                                        {
                                            // 创建Bitmap对象
                                            var bitmap = new Bitmap(new MemoryStream(imageBytes));

                                            // 假设你的Image控件名为imageControl
                                            image.Image.Source = bitmap;

                                            // 输出消息到控制台
                                            // Console.WriteLine(Core.NowMessage.Message);
                                        });
                                    }
                                    catch (Exception ex)
                                    {
                                        // Console.WriteLine($"Error converting Base64 to image: {ex.Message}");
                                    }
                                }
                            }   
                        }
                    }
                    ima = Core.NowMessage;
                }
            }
        });
    }
}