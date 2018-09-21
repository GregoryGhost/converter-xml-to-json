using System;
using System.Xml;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ConverterXML_JSON
{
	class Program
	{
		static void Main(string[] args)
		{
			var xmlStr = "";
			const int maxArgs = 2;
			var jsonFilePath = "";

			try
			{
				if (args.Length != maxArgs)
				{
					throw new ArgumentException(
                        "Arguments wrong.");
				}

				var xmlFile = args[0];
				jsonFilePath = args[1];

				if (!File.Exists(xmlFile))
				{
					throw new FileNotFoundException(
                        "Input xml file not found.");
				}

				xmlStr = File.ReadAllText(xmlFile);

				var doc = new XmlDocument();
				doc.LoadXml(xmlStr);

                var subLayers = doc.SelectNodes("Capability/Layer/Layer");

				var s = subLayers.Cast<XmlNode>()
						 .Where(n => n.Name.ToLower() == "layer")
								 .ToList()
								 .Select(sl =>
								 {
									 var subLayer = new SubLayer();
                                     
                                     sl.Cast<XmlNode>()
                                        .ToList()
                                        .ForEach(sNode =>
                                        {
                                            if (sNode.Name.ToLower() == "name")
                                            {
                                             subLayer.name = sNode.InnerText;
                                            }
                                            if (sNode.Name.ToLower() == "title")
                                            {
                                             subLayer.title = sNode.InnerText;
                                            }
                                            if(sNode.Name.ToLower()== "attributes")
                                            {
                                                 subLayer.attributes = sNode.Cast<XmlNode>()
                                                     .Select(a =>
                                                     {
                                                         return new Attribute
                                                         {
                                                            name = a.Attributes["name"]
                                                                ?.Value ?? "",
                                                            type = a.Attributes["type"]
                                                                ?.Value ?? ""
                                                         };
                                                     }).ToList();
                                            }
                                        });
                                            
                                    return subLayer;
								 }).ToList();

				var layer = new Layer
                {
                    name = doc.SelectSingleNode("Capability/Layer/Name")
                        ?.InnerText ?? "",
				    title = doc.SelectSingleNode("Capability/Layer/Title")
                        ?.InnerText ?? "",
                    sublayers = s
                };

                var fJson = JsonConvert.SerializeObject(layer)
                    .FormattingJson();

				Console.WriteLine("Given JSON:");
				Console.WriteLine(fJson);
				Console.WriteLine();

				File.WriteAllText(jsonFilePath, fJson);
				var msgConvertComplete = String.Format(
					"The conversion of the input" +
					" file was successfully completed.\n" +
					"The data is written to " +
					"a file along the path {0}",
					jsonFilePath);
				Console.WriteLine(msgConvertComplete);
			}
			catch (Exception e)
			{
				Console.WriteLine("Error:{0}", e.Message);
                Console.WriteLine("Stack trace:{0}", e.StackTrace);
			}
		}
	}

	public class Layer
	{
		public string name = "";
		public string title = "";
		public List<SubLayer> sublayers;
	}

	public class SubLayer
	{
		public string name = "";
		public string title = "";
		public List<Attribute> attributes;
	}

	public class Attribute
	{
		public string name = "";
		public string type = "";
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
