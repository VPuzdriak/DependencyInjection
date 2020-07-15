using System;

namespace DependencyInjection
{
    public interface IContainer : IDisposable
    {
        TService Resolve<TService>();
        object Resolve(Type type);
    }
}