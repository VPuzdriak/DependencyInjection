using System;

namespace DependencyInjection.Sample.Services.SomeService
{
    public interface ISomeService : IDisposable
    {
        public bool Disposed { get; }
        void PrintGuid();
    }
}