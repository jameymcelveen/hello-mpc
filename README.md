# hello-mpc

A hello world [Model Context Protocol](https://modelcontextprotocol.io) server in C#, using the official `ModelContextProtocol` SDK over stdio.

## Tools

| Tool | What it does |
|---|---|
| `hello` | Says hello to a name |
| `server_time` | Returns the time on the machine running the server |
| `list_rules` | Lists the rule sets in `RULES_DIR` |
| `get_rules` | Returns a rule set's Markdown, for agents to read at the start of a task |

The rules tools are the point: one server, one folder (later a database or API), and every project and agent reads the same rules.

## Run

Requires the .NET 8 SDK.

```sh
dotnet build
dotnet run
```

It speaks JSON-RPC on stdin/stdout, so it sits waiting for a client. Logs go to stderr.

To poke at it by hand, use the MCP Inspector:

```sh
npx @modelcontextprotocol/inspector dotnet run --project .
```

## Connect it

**Claude Code** (available in every project):

```sh
claude mcp add --scope user hello-mpc -- dotnet run --project ~/dev/hello-mpc/HelloMcp.csproj
```

**Claude Desktop** (`claude_desktop_config.json`) and **Cursor** (`~/.cursor/mcp.json`) use the same shape:

```json
{
  "mcpServers": {
    "hello-mpc": {
      "command": "dotnet",
      "args": ["run", "--project", "/Users/YOU/dev/hello-mpc/HelloMcp.csproj"],
      "env": { "RULES_DIR": "/Users/YOU/dev/hello-mpc/rules" }
    }
  }
}
```

Then ask: "Use the hello-mpc tool to say hello to Jamey," or "Get the house rules before you start."

## Next

- Read rules from a database or an HTTP API instead of files.
- Expose rules as MCP prompts so they show up as slash commands.
- Publish as a .NET tool (`dotnet tool install`) so the config is just `hello-mpc`.
