using System;
using DependencyInjection.Sample.Services.GuidGenerator;

namespace DependencyInjection.Sample.Services.SomeService
{
    public class SomeService : ISomeService
    {
        public IGuidGenerator GuidGenerator { get; }
        public bool Disposed { get; private set; }

        public SomeService()
        {
            GuidGenerator = new RandomGuidGenerator();
        }
        
        public SomeService(IGuidGenerator guidGenerator)
        {
            GuidGenerator = guidGenerator;
        }

        public void PrintGuid()
        {
            Console.WriteLine(GuidGenerator.Guid);
        }

        public void Dispose()
        {
            Disposed = true;
        }
    }
}