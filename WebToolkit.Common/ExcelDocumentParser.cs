using System;
using System.Data;
using System.IO;
using ExcelDataReader;
using WebToolkit.Contracts;

namespace WebToolkit.Common
{
    public class ExcelDocumentParser : IExcelDocumentParser
    {
        
        public DataSet ParseFile(string filename)
        {
            if(!File.Exists(filename))
                throw new ArgumentException("File does not exist");

            using (var stream = File.Open(filename, FileMode.Open, FileAccess.Read))
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                return reader.AsDataSet();
            }
        }
    }
}