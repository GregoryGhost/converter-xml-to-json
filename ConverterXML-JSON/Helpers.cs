using System;
using System.Xml;

namespace ConversionTools
{
    public interface IConversionFactory<T>
    {
        IParser<T> CreateParser();
        IMapper<T> CreateMapper();
    }
    

    public class ConversionFactory: IConversionFactory<XmlNode>
    {
        public IParser<XmlNode> CreateParser()
        {
            return new ParserXml();
        }

        public IMapper<XmlNode> CreateMapper()
        {
            return new MapperXmlToLayers();
        }
    }


    public static class Logger
    {
        public static void WriteSuccessConversion(string json,
            string pathToJsonFile)
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
