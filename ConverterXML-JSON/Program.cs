using System;
using System.Xml;
using Newtonsoft.Json;
using System.IO;

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
					throw new ArgumentException("Arguments wrong.");
                }

				var xmlFile = args[0];
				jsonFilePath = args[1];

				if(!File.Exists(xmlFile))
				{
					throw new FileNotFoundException("Input xml file not found.");
				}

				xmlStr = File.ReadAllText(xmlFile);

				var doc = new XmlDocument();
                doc.LoadXml(xmlStr);

                var json = JsonConvert.SerializeXmlNode(doc);
				var fJson = json.FormattingJson();
				Console.WriteLine("Given JSON:");
                Console.WriteLine(fJson);
                Console.WriteLine();

				File.WriteAllText(jsonFilePath, fJson);
				var msgConvertComplete = String.Format(
                    "The conversion of the input" +
                    " file was successfully completed.\n" +
                    " The data is written to " +
                    "a file along the path {0}",
                    jsonFilePath);
                Console.WriteLine(msgConvertComplete);
			}
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
			}
                     
			//var alex = new Test();
			//alex.Age = 12;
			//alex.Name = "Alex";
			//alex.SurName = "Nim";

			//var jAlex = JsonConvert.SerializeObject(alex);
			//Console.WriteLine(jAlex.FormattingJson());
		}
	}

    class Test
	{
		public string Name;
		public string SurName;
		public int Age;
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
