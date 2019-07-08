using System;
using WebToolkit.Common;
using Xunit;

namespace WebToolkit.Tests
{
    public class RetryHandleTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(10)]
        public void RetryHandle_Retries_x_times(int times)
        {
            var retryAttempt = 0;
            var sut = RetryHandle.Create(options =>
            {
                options.MaximumAttempts = times;
                options.Timeout = 1000;
            } ,typeof(TimeoutException));

            sut.Handle<string>(() => throw new TimeoutException(), ex => { retryAttempt++; });
            Assert.Equal(times, retryAttempt);
        }

        [Theory]
        [InlineData(1, "Hello")]
        [InlineData(3, "John Doe")]
        [InlineData(5, "Jane Doe")]
        [InlineData(10, "Yet, Another test")]
        public void RetryHandle_Retries_x_times_then_passes_on_last_attempt(int times, string passValue)
        {
            var retryAttempt = 0;
            var sut = RetryHandle.Create(options =>
            {
                options.MaximumAttempts = times;
                options.Timeout = 1000;
            } ,typeof(TimeoutException));

            Assert.Equal(passValue,
            sut.Handle<string>(() =>
            {
                if (retryAttempt == times - 1)
                    return passValue;
                throw new TimeoutException();
            }, ex => { retryAttempt++; }));

            Assert.Equal(times - 1, retryAttempt);
        }
    }
}