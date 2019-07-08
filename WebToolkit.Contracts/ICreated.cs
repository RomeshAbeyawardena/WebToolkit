using System;

namespace WebToolkit.Contracts
{
    public interface ICreated : ICreated<DateTimeOffset>
    {

    }

    public interface ICreated<TDate> where TDate : struct
    {
        TDate Created { get; set; }
    }
}