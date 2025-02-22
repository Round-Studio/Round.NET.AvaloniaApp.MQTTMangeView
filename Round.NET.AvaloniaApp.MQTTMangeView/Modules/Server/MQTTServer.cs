using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client.Receiving;
using MQTTnet.Protocol;
using MQTTnet.Server;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules.Entry;
using Round.NET.AvaloniaApp.MQTTMangeView.Modules.Logs;

namespace Round.NET.AvaloniaApp.MQTTMangeView.Modules.Server;

public class MQTTServer
{
    public static IMqttServer? mqttServer;
    public static bool IsRunning { get; private set; }

    public async static void Start()
    {
        // 创建MQTT服务器选项
        IMqttServerOptions? mqttServerOptions = new MqttServerOptionsBuilder()
            .WithDefaultEndpointPort(Project.Project.NowProject.Port) // 设置MQTT服务器端口
            .Build();

        // 创建MQTT服务器
        mqttServer = new MqttFactory().CreateMqttServer();

        // 处理客户端连接事件
        mqttServer.ClientConnectedHandler = new MqttServerClientConnectedHandlerDelegate(e =>
        {
            Log.WriteLine($"客户端连接: {e.ClientId}");
        });

        // 处理客户端断开连接事件
        mqttServer.ClientDisconnectedHandler = new MqttServerClientDisconnectedHandlerDelegate(e =>
        {
            Log.WriteLine($"客户端断开连接: {e.ClientId}");
        });

        // 处理消息接收事件
        mqttServer.ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(e =>
        {
            var message = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
            // GL.MQTT_New_Message = e.ApplicationMessage.Topic + "|" + message;
            Core.NowMessage = new MessageEntry()
            {
                Topic = e.ApplicationMessage.Topic,
                Message = message
            };
            Log.WriteLine($"接收到消息: 客户端 ID ：{e.ClientId}, 主题：{e.ApplicationMessage.Topic}, 消息：{message}");
        });

        Debug.WriteLine(mqttServerOptions.ToString());
        // 启动MQTT服务器
        await mqttServer.StartAsync(mqttServerOptions);
        IsRunning = true;
        Log.WriteLine(
            $"MQTT 服务器已启动 {mqttServerOptions.DefaultEndpointOptions.BoundInterNetworkAddress}:{mqttServerOptions.DefaultEndpointOptions.Port}");
    }

    public async static void Sendmesage(string topic, string messg)
    {
        // 创建MQTT消息
        var mqttMessage = new MqttApplicationMessageBuilder()
            .WithTopic(topic)
            .WithPayload(messg)
            .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
            .Build();
        // 发布消息
        await mqttServer.PublishAsync(mqttMessage);
        Log.WriteLine("消息已发送");
    }

    public async static void Stop()
    {
        if (mqttServer != null)
        {
            await mqttServer.StopAsync();
            IsRunning = false;
            Log.WriteLine("MQTT 服务器已停止");
        }
    }
}