using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebToolkit.Common;
using Xunit;

namespace WebToolkit.Tests
{
    public class RaceTests
    {
        async Task<decimal> SlowProcess(int delay)
        {
            await Task.Delay(delay);
            return 224959.42m;
        }

        [Fact]
        public async Task Race_Start()
        {
            var racer = new ParallelTask();
            var result = await racer.Start(SlowProcess(5000), async(slowProcess) =>
            {
                var amount = await SlowProcess(5500);
                amount += await slowProcess;

                return amount;
            } );

            Assert.Equal(449918.84m, result);
        }

        [Fact]
        public async Task Race_StartMany()
        {
            var racer = new ParallelTask();
            var result = await racer
                .StartMany(async (tasks) =>
                {
                    await Task.Delay(5500);
                    var res = await Task.WhenAll(tasks.ToArray());
                    return res.Sum();
                }, SlowProcess(1000), SlowProcess(2000), SlowProcess(3000), SlowProcess(4000), SlowProcess(5000));

            Assert.Equal(1124797.10m, result);
        }
    }
}