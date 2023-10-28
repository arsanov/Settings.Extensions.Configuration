# Settings.Extensions.Configuration
Extension methods to register settings

## Usage

Install nuget package
```bash
dotnet add package Settings.Extensions.Configuration
```

then add the following code

```csharp
using Settings.Extensions.Configuration;

...


services.AddSettings<SomeApplicationSettings>(configuration);


```

This will call `AddOptions` which will register default options types(IOptions<>, IOptionsSnapshot<>, IOptionsMonitor<>), but also register settings type as well.

I.e., unlike `AddOptions` when resolving `SomeApplicationSettings` there is no need to resolve `IOptions<SomeApplicationSettings>`, but instead you can simply write
```csharp
SomeService(SomeApplicationSettings settings)
{
...
}
```


By default the settings type name will be used to map settings, but this can be overridden by specifying name parameter of the `AddSettings` method.

```csharp
services.AddSettings<SomeSettings>(configuration, name:"DifferentName");
```

This extension method returns tuple of IServiceCollection, IConfiguration, so that you can chain multiple settings without passing configuration every time

```csharp

services.AddSettings<SomeSettings>(configuration)
    .AddSettings<OtherSettings>()
    .AddSettings<SomeElseSettings>();

```

It is possible to specify validation by passing validation delegate, for example:

```csharp
services
    .AddSettings<SomeSettings>(configuration, OptionsBuilderDataAnnotationsExtensions.ValidateDataAnnotations) // using data annotation
    .AddSettings<OtherSettings>(s => s.AddSingleton<IValidateOptions<OtherSettings>>(new CustomValidation<OtherSettings>>())); // custom validation

```
