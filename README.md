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

Пример вывода программы:

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
