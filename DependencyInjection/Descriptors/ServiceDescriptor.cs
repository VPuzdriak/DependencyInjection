using System;
using System.Linq;
using System.Reflection;
using DependencyInjection.Enums;

namespace DependencyInjection.Descriptors
{
    internal class ServiceDescriptor
    {
        public Type ServiceType { get; internal set; }
        public Type ImplementationType { get; }
        public ConstructorInfo ConstructorInfo { get; internal set; }
        public LifeTime LifeTime { get; internal set; }
        public object Implementation { get; internal set; }

        public ServiceDescriptor(Type serviceType, Type implementationType, LifeTime lifeTime)
        {
            ConstructorInfo[] constructorInfos = implementationType.GetConstructors();

            ServiceType = serviceType;
            ImplementationType = implementationType;
            ConstructorInfo = constructorInfos.Length == 1 ? constructorInfos.First() : null;
            LifeTime = lifeTime;
        }

        public ServiceDescriptor(Type serviceType, Type implementationType, LifeTime lifeTime, object implementation)
            : this(serviceType, implementationType, lifeTime)
        {
            Implementation = implementation;
        }
    }
}