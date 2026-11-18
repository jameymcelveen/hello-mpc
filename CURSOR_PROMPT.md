# hello-mpc - Cursor prompt to finish it

Paste everything below the line into Cursor (Agent mode) with `~/dev/hello-mpc` open.

---

You are finishing **hello-mpc**, a small, public, MIT-licensed hello world **MCP (Model Context Protocol) server in C#**. It already has `Program.cs`, `Tools/HelloTools.cs`, `Tools/RulesTools.cs`, `rules/house-rules.md`, `README.md`, `LICENSE`, `.gitignore` and `.editorconfig`. Read them all first. Keep the code small and idiomatic; this repo is a portfolio piece, so clarity beats cleverness. Show me a plan, then work in small steps and stop after each one.

## Constraints
- .NET 8, C# 12, nullable enabled. Official `ModelContextProtocol` NuGet package (currently 1.4.0) over stdio.
- stdout is the protocol channel: never write to stdout from app code. Logs go to stderr.
- Ask before adding any NuGet package not listed here.

## Steps

1. **Make it build and run.** `dotnet build` with zero warnings. If the SDK API differs from what the code assumes, fix the code to match the installed package and tell me what changed. Verify with the MCP Inspector (`npx @modelcontextprotocol/inspector dotnet run --project .`) that all four tools list and answer. Stop.

2. **Solution layout.** Move to:
   ```
   hello-mpc.sln
   src/HelloMcp/        (the server; move existing code here)
   tests/HelloMcp.Tests/ (xUnit)
   ```
   Keep `rules/` copied to the output directory. Update README paths. Stop.

3. **Make the rules folder testable.** Replace the static `RulesDir` lookup with an injected `IRulesStore` (interface) and a `FileRulesStore` implementation, registered in DI, still honoring the `RULES_DIR` env var. Tools take the store through DI. Keep the path-traversal guard. Stop.

4. **Tests (xUnit).** Cover: `hello` default and named, `list_rules` sorted and empty-folder cases, `get_rules` found, missing, and rejected names like `../secrets`. Use a temp directory per test. Add one end-to-end test that starts the server in-process with the SDK's client over an in-memory or stdio transport and calls `hello`, if the SDK supports it cleanly; skip it and tell me if not. Stop.

5. **Rules as MCP prompts.** Add a `[McpServerPromptType]` that exposes each rule set as a prompt, so clients can show them as slash commands. Test it. Stop.

6. **Package as a .NET tool.** Add `PackAsTool`, `ToolCommandName` = `hello-mpc`, package id `HelloMcp`, version `0.1.0`, README and license metadata. Document `dotnet pack` and `dotnet tool install -g --add-source ./nupkg HelloMcp`, and update the client config examples to just `"command": "hello-mpc"`. Stop.

7. **Repo polish.**
   - `Makefile`: `help`, `build`, `test`, `run`, `inspect` (MCP Inspector), `pack`, `install`, `format` (`dotnet format`), `clean`.
   - `.github/workflows/ci.yml`: restore, build with warnings as errors, `dotnet format --verify-no-changes`, test, on push and pull request, .NET 8.
   - `.github/dependabot.yml` for NuGet and GitHub Actions.
   - README badges: CI, MIT license, .NET 8, MCP. Owner is `jameymcelveen`, repo `hello-mpc`.
   - A short "How it works" section in the README: stdio transport, tool discovery by attribute, why logs go to stderr.
   Stop.

8. **Final check.** `make test` green, `dotnet format` clean, README commands copy-pasteable. Give me a one-paragraph summary I can put in the GitHub repo description.
