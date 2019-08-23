using System.Data;

namespace WebToolkit.Contracts
{
    public interface IExcelDocumentParser
    {
        DataSet ParseFile(string filename);
    }
}