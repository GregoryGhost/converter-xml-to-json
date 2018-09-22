using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.Collections.Generic;

namespace ConversionTools
{
    /// <summary>
    /// Парсер некой структуры данных.
    /// </summary>
    public interface IParser<T>
    {
        /// <summary>
        /// Найти ключевые узлы в структуре данных.
        /// </summary>
        /// <returns>Возвращает ключевые узлы.</returns>
        List<T> FindKeyNodes();

        /// <summary>
        /// Парсер некой структуры данных.
        /// </summary>
        List<T> KeyNodes { get; }

        /// <summary>
        /// Парсер некой структуры данных.
        /// </summary>
        string InputData { get; set; }
    }

    
    /// <summary>
    /// Парсер XML документа определенной структуры.
    /// </summary>
    public class ParserXml: IParser<XmlNode>
    {
        /// <summary>
        /// Ключевые теги в XML документе.
        /// </summary>
        private List<XmlNode> _keyNodes;

        /// <summary>
        /// Входные данные XML документа.
        /// </summary>
        private string _inputXml;

        /// <summary>
        /// XML документ.
        /// </summary>
        private XmlDocument _doc;

        /// <summary>
        /// Корневой тег.
        /// </summary>
        private const string _tagRoot = "Capability/Layer/*";
        
        /// <summary>
        /// Инициализация парсера XML документа определенной структуры.
        /// </summary>
        public ParserXml()
        {
            _doc = new XmlDocument();
        }
        
        /// <summary>
        /// Найти ключевые теги в XML документе.
        /// </summary>
        /// <returns>Возвращает ключевые теги.</returns>
        public List<XmlNode> FindKeyNodes()
        {
            _doc.LoadXml(_inputXml);
            _keyNodes = _doc.SelectNodes(_tagRoot)
                .Cast<XmlNode>().ToList();
            return _keyNodes;
        }

        /// <summary>
        /// Ключевые теги в XML документе.
        /// </summary>
        public List<XmlNode> KeyNodes
        {
            get
            {
                return this._keyNodes;
            }
            
            private set
            {
                this._keyNodes = value;
            }
        }
        
        /// <summary>
        /// Входные данные XML документа.
        /// </summary>
        public string InputData
        {
            get
            {
                return this._inputXml;
            }
            
            set
            {
                this._inputXml = value;
            }
        }
    }
}
