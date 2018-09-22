using System;
using System.Xml;

namespace ConversionTools
{
    /// <summary>
    /// Базовый класс для "Преобразователя"
    ///     из одного формата данных в другой.
    /// </summary>
    public abstract class ConverterBase<T>
    {
        /// <summary>
        /// Парсер некой структуры данных.
        /// </summary>
        protected IParser<T> _parser;

        /// <summary>
        /// Преобразователь узлов некой структуры
        ///     данных.
        /// </summary>
        protected IMapper<T> _mapper;

        /// <summary>
        /// Преобразованные данные.
        /// </summary>
        protected string _outputData;

        /// <summary>
        /// Инициализация "Преобразователя" данных.
        /// </summary>
        public ConverterBase(string inputData,
            IConversionFactory<T> cf)
        {
            this._parser = cf.CreateParser();
            this._parser.InputData = inputData;
            this._mapper = cf.CreateMapper();
        }
        
        /// <summary>
        /// Преобразовать данные
        ///     из одного формата в другой.
        /// </summary>
        public abstract string Process();

        /// <summary>
        /// Преобразованные данные.
        /// </summary>
        public abstract string OutputData { get; }
    }


    /// <summary>
    /// Преобразователь из XML в JSON.
    /// </summary>
    public class ConverterXmlToJson: ConverterBase<XmlNode>
    {
        /// <summary>
        /// Преобразователь из XML в JSON.
        /// </summary>
        /// <param name="xmlData">XML документ.</param>
        /// <param name="cf">Фабрика с класса
        ///     для конвертации данных.</param>
        public ConverterXmlToJson(string xmlData,
            ConversionFactory cf): base(xmlData, cf) { }

        /// <summary>
        /// Преобразовать из XML в JSON.
        /// </summary>
        /// <returns>Возвращает данные в
        ///     формате JSON.</returns>
        public override string Process()
        {
            _outputData = _mapper.MapNodes(
                _parser.FindKeyNodes());
                
            return _outputData;
        }
        
        /// <summary>
        /// Преобразованные данные в формате JSON.
        /// </summary>
        public override string OutputData => this._outputData;
    }
}

    
