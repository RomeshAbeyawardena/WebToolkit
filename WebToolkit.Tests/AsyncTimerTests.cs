using System;
using System.Linq;
using System.Threading.Tasks;
using WebToolkit.Common;
using Xunit;

namespace WebToolkit.Tests
{
    public class AsyncTimerTests
    {
        [Fact]
        public async Task Run_triggers()
        {
            var i = 0;
            var sut = AsyncTimer.Create(async (t) =>
            {
                if (i >= 10)
                    t.Stop();
                else i++;
                await Task.CompletedTask;
            }, 1000);

            sut.Elapsed += Sut_Elapsed;

            await sut.Run();

            Assert.Equal(10, i);
        }

        [Fact]
        public void SpanTest()
        {
            
            var counter = new Span<decimal>(new decimal[5000000]);
            

            for (var i = 0; i < 5000; i++)
                counter[i] = ((i * 0.2m + 0.5m) * 25.5m) / 100;

            var counterArray = counter.ToArray();

            Assert.Equal(638010, counterArray.Sum());
        }

        private void Sut_Elapsed(object sender, System.EventArgs e)
        {
            Assert.True(true);
        }
    }
}