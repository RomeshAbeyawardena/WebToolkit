using System;
using Microsoft.Extensions.Internal;
using WebToolkit.Contracts.Providers;

namespace WebToolkit.Common.Providers
{
    public class DateTimeProvider : IDateTimeProvider
    {
        private readonly ISystemClock _systemClock;
        public DateTimeOffset Now => _systemClock.UtcNow;

        public DateTimeProvider(ISystemClock systemClock)
        {
            _systemClock = systemClock;
        }
    }
}