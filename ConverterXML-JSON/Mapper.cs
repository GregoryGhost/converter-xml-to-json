using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ConversionTools
{
    /// <summary>
    /// Преобразователь узлов.
    /// </summary>
    public interface IMapper<T>
    {
        /// <summary>
        /// Преобразовать узлы.
        /// </summary>
        /// <returns>Возвращает преобразованные
        ///     неким образом узлы.</returns>
        string MapNodes(List<T> nodes);

        /// <summary>
        /// Преобразованные узлы.
        /// </summary>
        string OutputData { get; }
    }


    /// <summary>
    /// Преобразователь тегов XML.
    /// </summary>
    public class MapperXmlToLayers: IMapper<XmlNode>
    {
        /// <summary>
        /// Преобразованные теги XML.
        /// </summary>
        private Layer _outputData;
        
        /// <summary>
        /// Тег "Name".
        /// </summary>
        private const string _tagName = "name";
        
        /// <summary>
        /// Тег "Title".
        /// </summary>
        private const string _tagTitle = "title";

        /// <summary>
        /// Тег "Type".
        /// </summary>
        private const string _tagType = "type";
        
        /// <summary>
        /// Тег "Attributes".
        /// </summary>
        private const string _tagAttributes = "attributes";
        
        /// <summary>
        /// Тег "Layer".
        /// </summary>
        private const string _tagLayer = "layer";

        /// <summary>
        /// Преобразованные теги XML.
        /// </summary>
        public string OutputData =>
            JsonConvert.SerializeObject(_outputData)
                .FormattingJson();
                
        /// <summary>
        /// Преобразование тегов XML в JSON.
        /// </summary>
        /// <returns>Возвращает преобразованные
        ///     теги в JSON.</returns>
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
        
        /// <summary>
        /// Попытаться связать тег XML с объектом SubLayer.
        /// </summary>
        /// <param name="sNode">Узел XML.</param>
        /// <param name="subLayer">Объект SubLayer'а.</param>
        private void CheckNodes(XmlNode sNode,
            ref SubLayer subLayer)
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


    /// <summary>
    /// Тег "Слой".
    /// </summary>
    public class Layer
    {
        /// <summary>
        /// Аттрибут "Имя".
        /// </summary>
        public string name;
        
        /// <summary>
        /// Аттрибут "Заголовок".
        /// </summary>
        public string title;
        
        /// <summary>
        /// Тег "Дочерние слои".
        /// </summary>
        public List<SubLayer> sublayers;
    }


    /// <summary>
    /// Тег "Дочерний слои".
    /// </summary>
    public class SubLayer
    {
        /// <summary>
        /// Аттрибут "Имя".
        /// </summary>
        public string name;

        /// <summary>
        /// Аттрибут "Заголовок".
        /// </summary>
        public string title;

        /// <summary>
        /// Аттрибуты тега.
        /// </summary>
        public List<Attribute> attributes;
    }


    /// <summary>
    /// Аттрибуты кого-то тега.
    /// </summary>
    public class Attribute
    {
        /// <summary>
        /// Аттрибут "Имя".
        /// </summary>
        public string name;

        /// <summary>
        /// Аттрибут "Тип".
        /// </summary>
        public string type;
    }

    /// <summary>
    /// Расширение метода строки для форматирования JSON.
    /// </summary>
    public static class StrExt
    {
        /// <summary>
        /// Форматировать JSON.
        /// </summary>
        /// <returns>Возвращает форматирвоанный
        ///     теги в JSON.</returns>
        public static string FormattingJson(this string json)
        {
            dynamic parsedJson = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(parsedJson,
                Newtonsoft.Json.Formatting.Indented);
        }
    }
}
