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

# To do list

- Make OutdateChecked async;
- Extract logic away from Program.cs and write tests for it;
- Check for `frameworks` specific dependencies;
- Don't throw exception when `dependencies` key is missing inside project.json
