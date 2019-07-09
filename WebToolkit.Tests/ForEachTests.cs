using WebToolkit.Common.Extensions;
using Xunit;

namespace WebToolkit.Tests
{
    public class ForEachTests
    {
        [Fact]
        public void ForEach_affects_all_elements()
        {
            var total = 0;
            var items = new[] {1, 4, 6, 8, 12};
            items.ForEach(item => total += item);

            Assert.Equal(31, total);
        }

        [Fact]
        public void ForEach_affects_filtered_elements()
        {
            var total = 0;
            var items = new[] {1, 4, 6, 8, 12};
            items.ForEach(item => total += item, item => item < 12);

            Assert.Equal(19, total);
        }
    }
}