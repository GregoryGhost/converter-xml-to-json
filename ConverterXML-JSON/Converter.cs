using System;
using System.Xml;

namespace ConversionTools
{
    public abstract class ConverterBase<T>
    {
        protected IParser<T> _parser;
        protected IMapper<T> _mapper;
        protected string _outputData;
        
        public ConverterBase(string inputData,
            IConversionFactory<T> cf)
        {
            this._parser = cf.CreateParser();
            this._parser.InputData = inputData;
            this._mapper = cf.CreateMapper();
        }
        public abstract string Process();
        public abstract string OutputData { get; }
    }


    public class ConverterXmlToJson: ConverterBase<XmlNode>
    {
        public ConverterXmlToJson(string xmlData,
            ConversionFactory cf): base(xmlData, cf) { }

        public override string Process()
        {
            _outputData = _mapper.MapNodes(
                _parser.FindKeyNodes());
                
            return _outputData;
        }

        public override string OutputData => this._outputData;
    }
}

    
