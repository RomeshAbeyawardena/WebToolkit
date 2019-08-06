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
        public async Task ParallelTask_Start()
        {
            var parallelTask = new ParallelTask();
            var result = await parallelTask.Start(SlowProcess(5000), async(slowProcess) =>
            {
                var amount = await SlowProcess(5500);
                amount += await slowProcess;

                return amount;
            } );

            Assert.Equal(449918.84m, result);
        }

        [Fact]
        public async Task ParallelTask_StartMany()
        {
            var parallelTask = new ParallelTask();
            var result = await parallelTask
                .StartMany(async (tasks) =>
                {
                    await Task.Delay(6050);
                    var res = await Task.WhenAll(tasks.ToArray());
                    return res.Sum();
                }, SlowProcess(1000), 
                    SlowProcess(2000), 
                    SlowProcess(3000), 
                    SlowProcess(4000), 
                    SlowProcess(5000), 
                    SlowProcess(6000));

            Assert.Equal(1349756.52m, result);
        }
    }
}