﻿﻿﻿<h1 align="center">Calamity</h1><div align="center">

[![forthebadge](https://forthebadge.com/images/badges/fuck-it-ship-it.svg)](https://forthebadge.com)
[![forthebadge](https://forthebadge.com/images/badges/made-with-c-sharp.svg)](https://forthebadge.com)

[![GitHub license](https://img.shields.io/github/license/LegendaryB/Kirei.svg?longCache=true&style=flat-square)](https://github.com/LegendaryB/Kirei/blob/master/LICENSE)
[![Nuget](https://img.shields.io/nuget/v/Calamity.svg?style=flat-square)](https://www.nuget.org/packages/Calamity/)

A (mini) plugin framework for .NET applications.
<br>
<br>
<sub>Built with ❤︎ by Daniel Belz</sub>
</div><br>

## Getting started

### Configuration
The static `PluginLoaderOptions` class contains properties for the configuration of Calamity. The table should be self explaining.
|Property|Description|Default value|
|---|---|---|
|LoggerFactory|Factory to provide a logger for the internal library types.|NullLogger|
|TypeActivator|The default `ITypeActivator` which is used to create object instances. |.NET Framework Activator|
|PreferAssembliesFromHost|Flag to indicate if the plugin should prefer assemblies from the host.|true|

### Loading a plugin from a assembly
To load a plugin from a assembly you need to use the static `PluginLoaderFactory` class. The method `CreateLoaderFor<>` will return a generic `IPluginLoader` instance to you. This instance holds all metadata which is then required to create a instance of the plugin. After setting several properties on your `IPluginLoader` instance you can finally use the `Build()` method. If you don't specify an alternate `ITypeActivator` implementation the default one from the `PluginLoaderOptions` will be used instead.

```csharp
class Program
{
    static void Main()
    {
        var path = @"C:\MyPluginAssembly.dll";
        
        // Will create a instance of a type which implements ITestPlugin and uses the default ITypeActivator.
        var instance = PluginLoaderFactory
            .CreateLoaderFor<ITestPlugin>(path)
            .Build();
            
        // Will create a instance of a type which implements ITestPlugin and uses the specified ITypeActivator.
        var instance = PluginLoaderFactory
            .CreateLoaderFor<ITestPlugin>(path)
            .Build(new MyTypeActivator());  
            
        // Will create a instance of a type which implements ITestPlugin and uses the default ITypeActivator in combination with constructor parameters.
        var instance = PluginLoaderFactory
            .CreateLoaderFor<ITestPlugin>(path)
            .AddConstructorParameters("param1", "param2")
            .Build();
    }
}
```

## Contributing

__Contributions are always welcome!__  
When you send me a pull request please make sure to add some information regarding your changes, improvements or bugfixes.

## License

This project is licensed under the MIT license - see the [LICENSE](LICENSE) file for details
