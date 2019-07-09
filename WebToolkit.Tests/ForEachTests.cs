using System.Linq;
using WebToolkit.Common.Extensions;
using Xunit;

namespace WebToolkit.Tests
{
    public class ForEachTests
    {
        [Fact]
        public void ForEach_affects_all_elements()
        {
            var items = new[] {1, 4, 6, 8, 12};
            var newItems = items.ForEach(item => item * 2).ToArray();

            Assert.Contains(2, newItems);
            Assert.Contains(8, newItems);
            Assert.Contains(16, newItems);
            Assert.Contains(24, newItems);
        }

        [Fact]
        public void ForEach_affects_filtered_elements()
        {
            var total = 0;
            var items = new[] {1, 4, 6, 8, 12};
            items.ForEach(item =>
            {
                total += item;
            }, item => item < 12);

            Assert.Equal(19, total);
        }
    }
}