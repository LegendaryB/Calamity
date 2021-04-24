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
The `PluginLoaderOptions` class contains properties for the configuration of Calamity. The table shows a list of properties and a description what they do.
|Name|Description|Default value|
|---|---|---|
|LoggerFactory|Factory to provide a logger for the internal library types.|NullLogger|
|TypeActivator|The default `ITypeActivator` which is used to create object instances. |.NET Framework Activator|
|PreferAssembliesFromHost|Flag to indicate if the plugin should prefer assemblies from the host.|true|

## Contributing

__Contributions are always welcome!__  
When you send me a pull request please make sure to add some information regarding your changes, improvements or bugfixes.

## License

This project is licensed under the MIT license - see the [LICENSE](LICENSE) file for details
