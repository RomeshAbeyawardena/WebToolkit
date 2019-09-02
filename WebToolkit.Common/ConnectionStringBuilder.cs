using System.Collections.Generic;
using System.Linq;
using WebToolkit.Contracts;

namespace WebToolkit.Common
{
    public class ConnectionStringBuilder : IConnectionStringBuilder
    {
        public string BuildConnectionString(string server, string database, bool? trustedConnection = null,
            bool? multipleActiveResultSets = null, IDictionary<string, string> additionalKeyValues = null)
        {
            var connectionString = $"server={server};database={database};";

            if (trustedConnection.HasValue)
                connectionString += $"Trusted_Connection={trustedConnection};";

            if (multipleActiveResultSets.HasValue)
                connectionString += $"MultipleActiveResultSets={multipleActiveResultSets};";

            if (additionalKeyValues == null) 
                return connectionString;

            foreach (var keyValuePair in additionalKeyValues)
            {
                connectionString += $"{keyValuePair.Key}={keyValuePair.Value};";
            }

            return connectionString;
        }
    }
}