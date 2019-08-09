using WebToolkit.Common.Extensions;
using Xunit;

namespace WebToolkit.Tests
{
    public class StringExtensionTests
    {
        [Theory]
        [InlineData("Apples,Apricots,Blue Berries,Banana's,Cherry,Cape gooseberry", 
            "Apricots,Blue Berries,Cherry,", 
            new [] { "Apples,", "Banana's,", "Cape gooseberry" })]
        public void ReplaceAll_replaces_specified_values(string stringList, string expected, string[] replacedValues)
        {
            var result = stringList.ReplaceAll(string.Empty, replacedValues);
            Assert.Equal(expected, result);
        }
    }
}