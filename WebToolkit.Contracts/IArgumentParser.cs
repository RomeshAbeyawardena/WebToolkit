using System.Collections.Generic;

namespace WebToolkit.Contracts
{
    public interface IArgumentParser
    {
        IDictionary<string, string> GetArguments(char[] splitString, string[] args);
    }
}