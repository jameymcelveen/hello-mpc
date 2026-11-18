using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

// hello-mpc: a hello world MCP server over stdio.
// stdout carries the JSON-RPC protocol, so every log line must go to stderr.
var builder = Host.CreateApplicationBuilder(args);

builder.Logging.AddConsole(options =>
    options.LogToStandardErrorThreshold = LogLevel.Trace);

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

await builder.Build().RunAsync();
