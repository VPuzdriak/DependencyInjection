using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DependencyInjection.Builders;
using DependencyInjection.Descriptors;
using DependencyInjection.Enums;

namespace DependencyInjection
{
    public class Container : IContainer
    {
        private readonly IList<ServiceDescriptor> _descriptors;

        internal Container(IList<ServiceDescriptor> descriptors)
        {
            _descriptors = descriptors;
        }

        public TService Resolve<TService>()
        {
            return (TService) Resolve(typeof(TService));
        }

        public object Resolve(Type type)
        {
            ServiceDescriptor descriptor = GetDescriptor(type);

            if (descriptor.Implementation != null)
            {
                return descriptor.Implementation;
            }

            object implementation = CreateImplementation(descriptor);

            if (descriptor.LifeTime == LifeTime.Singleton)
            {
                descriptor.Implementation = implementation;
            }

            return implementation;
        }

        public void Dispose()
        {
            IEnumerable<IDisposable> disposableObjects = _descriptors
                .Where(d => d.Implementation != null)
                .Where(d => d.Implementation is IDisposable)
                .Select(d => (IDisposable) d.Implementation);

            foreach (IDisposable disposableObject in disposableObjects)
            {
                disposableObject.Dispose();
            }
        }

        private ServiceDescriptor GetDescriptor(Type serviceType)
        {
            return _descriptors.SingleOrDefault(d => d.ServiceType == serviceType)
                   ?? throw new Exception($"Service of type {serviceType.FullName} is not registered");
        }

        private object CreateImplementation(ServiceDescriptor descriptor)
        {
            object[] constructorArgs = GetConstructorInfo(descriptor).GetParameters()
                .Select(p => Resolve(p.ParameterType))
                .ToArray();

            return Activator.CreateInstance(descriptor.ImplementationType, constructorArgs);
        }

        private ConstructorInfo GetConstructorInfo(ServiceDescriptor descriptor)
        {
            return descriptor.ConstructorInfo ??
                   throw new Exception(
                       $"Constructor of type {descriptor.ImplementationType.FullName} cannot be inferred. Use {nameof(ServiceDescriptorBuilder)}.{nameof(ServiceDescriptorBuilder.UsingConstructor)} method to select constructor");
        }
    }
}