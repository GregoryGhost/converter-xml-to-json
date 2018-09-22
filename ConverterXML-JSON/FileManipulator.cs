using System;
using System.IO;

namespace ConversionTools
{
    /// <summary>
    /// Помощник по загрузке и сохранению
    ///     файлов.
    /// </summary>
    public class FileManipulator
    {
        /// <summary>
        /// Путь до входного XML файла.
        /// </summary>
        private string _pathToXmlFile;

        /// <summary>
        /// Путь до выходного JSON файла.
        /// </summary>
        private string _pathToJsonFile = "test.json";

        /// <summary>
        /// Входные данные XML документа.
        /// </summary>
        private string _inputXml;

        /// <summary>
        /// Максимальное количество аргументов
        ///     командной стркои.
        /// </summary>
        private const int _maxArgs = 2;

        /// <summary>
        /// Минимальное количество аргументов
        ///     командной стркои.
        /// </summary>
        private const int _minArgs = 1;

        /// <summary>
        /// Инициализация помощника.
        /// </summary>
        /// <param name="args">Аргументы
        ///     командной строки.</param>
        public FileManipulator(string[] args)
        {
            if (args.Length > _maxArgs
                || args.Length < _minArgs)
            {
                throw new ArgumentException(
                    "Invalid number of arguments");
            }
            
            _pathToXmlFile = args[0];
            
            if (args.Length == _maxArgs)
            {
                _pathToJsonFile = args[1];
            }
        }

        /// <summary>
        /// Входные данные XML документа.
        /// </summary>
        public string InputXml => this._inputXml;

        /// <summary>
        /// Путь до XML файла.
        /// </summary>
        public string PathXmlFile => this._pathToXmlFile;


        /// <summary>
        /// Путь до JSON файла.
        /// </summary>
        public string PathJsonFile => this._pathToJsonFile;

        /// <summary>
        /// Считать данные XML файла.
        /// </summary>
        public void ReadData()
        {
            if (!File.Exists(this._pathToXmlFile))
            {
                var msg = String.Format(
                    "The wrong path to the " +
                    "input file is specified.\nPath: {0}",
                    this._pathToXmlFile);
                throw new FileNotFoundException(msg);
            }
            this._inputXml = File.ReadAllText(
                this._pathToXmlFile);
        }

        /// <summary>
        /// Записать выходные данные.
        /// </summary>
        /// <param name="outputData">
        ///     Преобразованные данные.</param>
        public void WriteData(string outputData)
        {
            File.WriteAllText(this._pathToJsonFile,
                outputData);
        }
    }
}
