using System;

namespace DependencyInjection.Sample.Services.GuidGenerator
{
    public class RandomGuidGenerator : IGuidGenerator
    {
        public Guid Guid { get; } = Guid.NewGuid();
    }
}