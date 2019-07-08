using System;

namespace WebToolkit.Contracts
{
    public interface IModified : IModified<DateTimeOffset>
    {

    }

    public interface IModified<TDate> where TDate : struct
    {
        TDate Modified { get; set; }
    }
}