using System;
using System.Reflection;
using DependencyInjection.Descriptors;
using DependencyInjection.Enums;

namespace DependencyInjection.Builders
{
    public class ServiceDescriptorBuilder
    {
        internal ServiceDescriptor Descriptor { get; }

        public ServiceDescriptorBuilder(object implementation)
        {
            Type serviceType = implementation.GetType();
            Descriptor = new ServiceDescriptor(serviceType, serviceType, LifeTime.Singleton, implementation);
        }

        public ServiceDescriptorBuilder(Type serviceType)
        {
            if (serviceType.IsAbstract || serviceType.IsInterface)
            {
                throw new Exception(
                    $"Abstract class/interface {serviceType.FullName} cannot be used as implementation");
            }

            Descriptor = new ServiceDescriptor(serviceType, serviceType, LifeTime.Singleton);
        }

        public ServiceDescriptorBuilder As<TService>()
        {
            Type serviceType = typeof(TService);

            if (Descriptor.ImplementationType.GetInterface(serviceType.FullName) == null &&
                !Descriptor.ImplementationType.IsSubclassOf(serviceType))
            {
                throw new Exception(
                    $"{Descriptor.ImplementationType.FullName} is not subclass/implementor of {serviceType.FullName}");
            }

            Descriptor.ServiceType = serviceType;
            return this;
        }

        public ServiceDescriptorBuilder SingleInstance()
        {
            Descriptor.LifeTime = LifeTime.Singleton;
            return this;
        }

        public ServiceDescriptorBuilder InstancePerDependency()
        {
            Descriptor.LifeTime = LifeTime.Transient;
            return this;
        }

        public ServiceDescriptorBuilder UsingConstructor(params Type[] constructorArgTypes)
        {
            ConstructorInfo constructorInfo = Descriptor.ImplementationType.GetConstructor(constructorArgTypes);

            if (constructorInfo == null)
            {
                throw new Exception(
                    $"Appropriate constructor for type {Descriptor.ImplementationType.FullName} not found");
            }

            Descriptor.ConstructorInfo = constructorInfo;

            return this;
        }
    }
}