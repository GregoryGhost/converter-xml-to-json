using System;
using System.Xml;

namespace ConversionTools
{
    /// <summary>
    /// Фабрика с объектами для
    ///     конвертации данных.
    /// </summary>
    public interface IConversionFactory<T>
    {
        /// <summary>
        /// Создать парсер.
        /// </summary>
        /// <returns>Возвращает парсер.</returns>
        IParser<T> CreateParser();
        
        /// <summary>
        /// Создает "Преобразователь".
        /// </summary>
        /// <returns>Возвращает преобразователь.</returns>
        IMapper<T> CreateMapper();
    }

    
    /// <summary>
    /// Фабрика с объектами для
    ///     конвертации из XML в JSON.
    /// </summary>
    public class ConversionFactory: IConversionFactory<XmlNode>
    {
        /// <summary>
        /// Создать парсер для XML документа.
        /// </summary>
        /// <returns>Возвращает парсер
        ///     для XML документа.</returns>
        public IParser<XmlNode> CreateParser()
        {
            return new ParserXml();
        }

        /// <summary>
        /// Создать преобразователь для XML документа.
        /// </summary>
        /// <returns>Возвращает преобразователь
        ///     для XML документа.</returns>
        public IMapper<XmlNode> CreateMapper()
        {
            return new MapperXmlToLayers();
        }
    }


    /// <summary>
    /// Стеганографист.
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Записать в лог успешность конвертации.
        /// </summary>
        /// <param name="json">Данные в формате
        ///     JSON.</param>
        /// <param name="pathToJsonFile">Путь до
        ///     JSON файла.<param>
        public static void WriteSuccessConversion(
            string json, string pathToJsonFile)
        {
            Console.WriteLine("Given JSON:");
            Console.WriteLine(json);

            Console.WriteLine();

            var msgConvertComplete = String.Format(
                "The conversion of the input" +
                " file was successfully completed.\n" +
                "The data is written to " +
                "a file along the path {0}",
                pathToJsonFile);
            Console.WriteLine(msgConvertComplete);
        }
    }
}
