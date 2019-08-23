using System;
using System.Collections.Generic;
using System.Linq;
using WebToolkit.Contracts;

namespace WebToolkit.Common
{
    public class ArgumentParser : IArgumentParser
    {
        public IDictionary<string, string> GetArguments(char[] splitString, string[] args)
        {
            return args.Select(arg => arg
                .Split(splitString, StringSplitOptions.RemoveEmptyEntries))
                .Where(splitArgs => splitArgs.Length == 2)
                .ToDictionary(splitArgs => splitArgs[0], splitArgs => splitArgs[1]);
        }
    }
}