using System;
using ConversionTools;

namespace ConverterXML_JSON
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var fm = new FileManipulator(args);
                fm.ReadData();
                
                var cf = new ConversionFactory();
                var converter = new ConverterXmlToJson(
                    fm.InputXml, cf);
                converter.Process();
                
                fm.WriteData(converter.OutputData);
                
                Logger.WriteSuccessConversion(
                    converter.OutputData, fm.PathJsonFile);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:{0}", e.Message);
                Console.WriteLine("Stack trace:{0}", e.StackTrace);
            }
        }
    }
}
