using System.Collections.Generic;

namespace WebToolkit.Contracts
{
    public interface IConnectionStringBuilder
    {
        string BuildConnectionString(string server, string database, 
            bool? trustedConnection = null, bool? multipleActiveResultSets = null, 
            IDictionary<string, string> additionalKeyValues = null);
    }
}