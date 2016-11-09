dotnet-outdated
===

`dotnet-outdated` is a tool to check for outdated .NET Core dependencies.

### How To Install

Add `DotNetOutdated` to the `tools` section of your `project.json` file:

```
{
...
  "tools": {
    "DotNetOutdated": "1.2.1"
  }
...
}
```

### How To Use

```
dotnet outdated [options]

Options:
  -pre          Check for prereleases versions
```