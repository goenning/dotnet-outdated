dotnet-outdated
===

`dotnet-outdated` is a tool to check for outdated .NET Core dependencies.

### How To Install

Add `DotNetOutdated` as `DotNetCliToolReference` to your `.csproj` file:

```
<DotNetCliToolReference Include="DotNetOutdated" Version="x.x.x" />
```

### How To Use

```
dotnet outdated
```

### An example of what to expect

![](demo.png)

`Yellow` is for non-major version available to update. It's generally safe to update so you should do it.

`Red` is for new **MAJOR** update which may possible break something in your code. You should read the docs before updating.