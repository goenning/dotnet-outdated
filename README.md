dotnet-outdated
===

`dotnet-outdated` tool to check for outdated DotNet Core dependencies.

### How To Install

Add `DotNetOutdated` to the `tools` section of your `project.json` file:

```
{
...
  "tools": {
    "DotNetOutdated": "1.0.0"
  }
...
}
```

### How To Use

    dotnet outdated

# ToDo List

1) Mock NuGetClient during tests
2) Use real async HttpRequest
3) Extract logic away from Program.cs and write tests for it
