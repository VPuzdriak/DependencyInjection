using System;

namespace DependencyInjection.Sample.Services.GuidGenerator
{
    public interface IGuidGenerator
    {
        Guid Guid { get; }
    }
}