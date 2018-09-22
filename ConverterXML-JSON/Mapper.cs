using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ConversionTools
{
    public interface IMapper<T>
    {
        string MapNodes(List<T> nodes);

        string OutputData { get; }
    }


    public class MapperXmlToLayers: IMapper<XmlNode>
    {
        private Layer _outputData;

        private const string _tagName = "name";

        private const string _tagTitle = "title";

        private const string _tagType = "type";

        private const string _tagAttributes = "attributes";

        private const string _tagLayer = "layer";

        public string OutputData =>
            JsonConvert.SerializeObject(_outputData)
                .FormattingJson();

        public string MapNodes(List<XmlNode> nodes)
        {
            var sublayers = nodes
                .Where(n =>
                {
                    return n.Name.ToLower() == _tagLayer;
                })
                .Select(sl =>
                {
                    var subLayer = new SubLayer();

                    sl.Cast<XmlNode>()
                        .ToList()
                        .ForEach(sNode =>
                        {
                            CheckNodes(sNode, ref subLayer);
                        });
                            
                    return subLayer;
                }).ToList();

            _outputData = new Layer
            {
                name = nodes
                    .Where(n => n.Name.ToLower() == _tagName)
                    .DefaultIfEmpty()
                    .First()
                    ?.InnerText ?? "",
                title = nodes
                    .Where(n => n.Name.ToLower() == _tagTitle)
                    .DefaultIfEmpty()
                    .First()
                    ?.InnerText ?? "",
                sublayers = sublayers
            };

            return this.OutputData;
        }

        private void CheckNodes(XmlNode sNode, ref SubLayer subLayer)
        {
            if (sNode.Name.ToLower() == _tagName)
            {
                subLayer.name = sNode.InnerText;
            }
            
            if (sNode.Name.ToLower() == _tagTitle)
            {
                subLayer.title = sNode.InnerText;
            }
            
            if(sNode.Name.ToLower()== _tagAttributes)
            {
                subLayer.attributes = sNode.Cast<XmlNode>()
                    .Select(a =>
                    {
                        return new Attribute
                        {
                            name = a.Attributes[_tagName]
                                ?.Value ?? "",
                            type = a.Attributes[_tagType]
                                ?.Value ?? ""
                        };
                    }).ToList();
            }
        }
    }


    public class Layer
    {
        public string name;
        
        public string title;
        
        public List<SubLayer> sublayers;
    }


    public class SubLayer
    {
        public string name;
        
        public string title;
        
        public List<Attribute> attributes;
    }


    public class Attribute
    {
        public string name;
        
        public string type;
    }


    public static class StrExt
    {
        public static string FormattingJson(this string json)
        {
            dynamic parsedJson = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(parsedJson,
                Newtonsoft.Json.Formatting.Indented);
        }
    }
}
