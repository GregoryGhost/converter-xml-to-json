using System;
using System.IO;

namespace ConversionTools
{
    public class FileManipulator
    {
        private string _pathToXmlFile;

        private string _pathToJsonFile = "test.json";

        private string _inputXml;

        private const int _maxArgs = 2;

        private const int _minArgs = 1;
        
        public FileManipulator(string[] args)
        {
            if (args.Length > _maxArgs
                || args.Length < _minArgs)
            {
                throw new ArgumentException(
                    "Invalid number of arguments");
            }
            
            _pathToXmlFile = args[0];
            
            if (args.Length == _maxArgs)
            {
                _pathToJsonFile = args[1];
            }
        }

        public string InputXml => this._inputXml;

        public string PathXmlFile => this._pathToXmlFile;

        public string PathJsonFile => this._pathToJsonFile;

        public void ReadData()
        {
            if (!File.Exists(this._pathToXmlFile))
            {
                var msg = String.Format(
                    "The wrong path to the " +
                    "input file is specified.\nPath: {0}",
                    this._pathToXmlFile);
                throw new FileNotFoundException(msg);
            }
            this._inputXml = File.ReadAllText(
                this._pathToXmlFile);
        }

        public void WriteData(string outputData)
        {
            File.WriteAllText(this._pathToJsonFile,
                outputData);
        }
    }
}
