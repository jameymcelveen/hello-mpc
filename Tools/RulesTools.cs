using System.ComponentModel;
using ModelContextProtocol.Server;

namespace HelloMcp.Tools;

/// <summary>
/// Serves shared coding rules from one place, so every project and every agent
/// (Claude Code, Cursor, Claude Desktop) reads the same rules.
/// Rules live as Markdown files in RULES_DIR (default: ./rules next to the binary).
/// Swap the file reads for a database or HTTP call later; the tools stay the same.
/// </summary>
[McpServerToolType]
public static class RulesTools
{
    private static string RulesDir =>
        Environment.GetEnvironmentVariable("RULES_DIR")
        ?? Path.Combine(AppContext.BaseDirectory, "rules");

    [McpServerTool(Name = "list_rules"), Description("List the available rule sets by name.")]
    public static string[] ListRules() =>
        Directory.Exists(RulesDir)
            ? Directory.GetFiles(RulesDir, "*.md")
                .Select(Path.GetFileNameWithoutExtension)
                .OfType<string>()
                .Order()
                .ToArray()
            : [];

    [McpServerTool(Name = "get_rules"), Description("Get the full text of a rule set. Call this at the start of a task.")]
    public static string GetRules(
        [Description("Rule set name from list_rules, for example 'house-rules'.")] string name = "house-rules")
    {
        // Only allow simple names, so a caller can't read files outside RULES_DIR.
        if (name.Any(c => !(char.IsLetterOrDigit(c) || c is '-' or '_')))
            return $"Invalid rule set name: '{name}'.";

        var path = Path.Combine(RulesDir, name + ".md");
        return File.Exists(path)
            ? File.ReadAllText(path)
            : $"No rule set named '{name}'. Available: {string.Join(", ", ListRules())}";
    }
}
