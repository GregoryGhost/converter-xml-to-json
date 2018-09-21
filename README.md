# О проекте

Пример программы для конвертации некой заданной структуры из XML в JSON.

### Сборка проекта
В папке проекта запустить из терминала следующую команду:

```sh
$ dotnet run input.xml output.json
```
Принимаемые параметры:
 - `input.xml` - путь до входноо файла XML, имеющий заданную структуру тегов, похожую на пример XML файла ([input.xml][inputXml]) в SamplesData,
 - `output.json` - путь до выходного файла JSON после завершения процесса конвертации, имеющий заданную структуру тегов, похожую на пример JSON файлов ([output.json][outputJson], [test.json][testJson]) в SamplesData.

### Публикация проекта
В папке проекта запустить из терминала следующую команду:

```sh
$ sudo dotnet publish -c Release -r ubuntu.16.10-x64
```

Принимаемые параметры:
 - `ubuntu.16.10-x64` - [RID][rid] для исполняемого файла под Ubuntu OS.

Список поддерживаемых RID данным проектом в секции `RuntimeIdentifiers` находиться [здесь][ridProject].

[inputXml]: <ConverterXML-JSON/SamplesData/input.xml>
[outputJson]: <ConverterXML-JSON/SamplesData/output.json>
[testJson]: <ConverterXML-JSON/SamplesData/test.json>
[rid]: <https://docs.microsoft.com/ru-ru/dotnet/core/rid-catalog>
[ridProject]: <ConverterXML-JSON/ConverterXML-JSON.csproj>
