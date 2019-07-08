using System;

namespace WebToolkit.Contracts.Providers
{
    public interface IDateTimeProvider
    {
        DateTimeOffset Now { get; }
    }
}