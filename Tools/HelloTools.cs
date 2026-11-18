using System.ComponentModel;
using ModelContextProtocol.Server;

namespace HelloMcp.Tools;

[McpServerToolType]
public static class HelloTools
{
    [McpServerTool(Name = "hello"), Description("Say hello to someone.")]
    public static string Hello(
        [Description("Who to greet.")] string name = "world") =>
        $"Hello, {name}! Greetings from hello-mpc.";

    [McpServerTool(Name = "server_time"), Description("Current date and time on the machine running this server.")]
    public static string ServerTime() =>
        DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss zzz");
}
