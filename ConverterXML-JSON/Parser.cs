using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.Collections.Generic;

namespace ConversionTools
{
    public interface IParser<T>
    {
        List<T> FindKeyNodes();
        
        List<T> KeyNodes { get; }

        string InputData { get; set; }
    }
    

    public class ParserXml: IParser<XmlNode>
    {
        private List<XmlNode> _keyNodes;

        private string _inputXml;

        private XmlDocument _doc;

        private const string _tagRoot = "Capability/Layer/Layer";
        
        public ParserXml()
        {
            _doc = new XmlDocument();
        }
        
        public List<XmlNode> FindKeyNodes()
        {
            _doc.LoadXml(_inputXml);
            _keyNodes = _doc.SelectNodes(_tagRoot)
                .Cast<XmlNode>().ToList();
            return _keyNodes;
        }

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
