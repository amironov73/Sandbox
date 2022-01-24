### Sandbox

Песочница для иллюстрации работы пакета [Nerdbank.GitVersioning](https://github.com/dotnet/Nerdbank.GitVersioning).

Приложение не делает ничего полезного, оно просто вызывает библиотеку, которая в свою очередь просто сообщает номер версии.

### Итак, как же работает GitVersioning?

Сначала нам необходимо установить соответствующую утилиту:

```shell
dotnet tool install -g nbgv
```

Если не хочется устанавливать глобально, можно воспользоваться локальным вариантом;

```shell
dotnet tool install --tool-path . nbgv
```

Далее, в директории решения инициализируем GitVersioning:

```shell
nbgv install
```

Эта команда создает файлы `Directory.Build.props` 

```msbuild
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="Current" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <ItemGroup>
    <PackageReference Include="Nerdbank.GitVersioning" Condition="!Exists('packages.config')">
      <Version>3.4.255</Version>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
  </ItemGroup>
</Project>
```

и `version.json`

```json
{
  "$schema": "https://raw.githubusercontent.com/dotnet/Nerdbank.GitVersioning/master/src/NerdBank.GitVersioning/version.schema.json",
  "version": "1.0-beta",
  "publicReleaseRefSpec": [
    "^refs/heads/master$",
    "^refs/heads/v\\d+(?:\\.\\d+)?$"
  ],
  "cloudBuild": {
    "buildNumber": {
      "enabled": true
    }
  }
}
```

Пример вывода программы. До коммита:

```
File:             D:\Projects\Sandbox\Application\bin\Debug\net6.0\Library.dll
InternalName:     Library.dll
OriginalFilename: Library.dll
FileVersion:      1.0.0.55743
FileDescription:  Library
Product:          Library
ProductVersion:   1.0.0-beta+bfd91e4cb0
Debug:            False
Patched:          False
PreRelease:       False
PrivateBuild:     False
SpecialBuild:     False
Language:         Независимо от языка

```

После коммита:

```
File:             D:\Projects\Sandbox\Application\bin\Debug\net6.0\Library.dll
InternalName:     Library.dll
OriginalFilename: Library.dll
FileVersion:      1.0.1.14288
FileDescription:  Library
Product:          Library
ProductVersion:   1.0.1-beta+d03748a4a4
Debug:            False
Patched:          False
PreRelease:       False
PrivateBuild:     False
SpecialBuild:     False
Language:         Независимо от языка
```

Каждый коммит будет увеличивать номер версии.

Кроме того, в сборку автоматически добавляется класс `ThisAssembly` следующего вида (конкретные номера версий и версия сборки приведены лишь в иллюстративных целях):

```c#
internal sealed partial class ThisAssembly 
{
    internal const string AssemblyVersion = "1.0";
    internal const string AssemblyFileVersion = "1.0.24.15136";
    internal const string AssemblyInformationalVersion = "1.0.24-alpha+g9a7eb6c819";
    internal const string AssemblyName = "Microsoft.VisualStudio.Validation";
    internal const string PublicKey = @"0024000004800000940000...reallylongkey..2342394234982734928";
    internal const string PublicKeyToken = "b03f5f7f11d50a3a";
    internal const string AssemblyTitle = "Microsoft.VisualStudio.Validation";
    internal const string AssemblyConfiguration = "Debug";
    internal const string RootNamespace = "Microsoft";
}
```

Когда мы готовы к релизу новой версии, подаем команду

```shell
nbgv prepare-release
```

на что утилита нам отвечает

```
v1.0 branch now tracks v1.0 stabilization and release.
main branch now tracks v1.1-alpha development.
```

Пушим и радуемся! Согласитесь, очень удобно!
