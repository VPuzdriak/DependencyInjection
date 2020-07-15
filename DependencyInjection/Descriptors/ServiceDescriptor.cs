using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DependencyInjection.Enums;

namespace DependencyInjection.Descriptors
{
    internal class ServiceDescriptor
    {
        public Type ServiceType { get; internal set; }
        public Type ImplementationType { get; }
        public IList<Type> ConstructorArgTypes { get; internal set; }
        public LifeTime LifeTime { get; internal set; }
        public object Implementation { get; internal set; }

        public ServiceDescriptor(Type serviceType, Type implementationType, LifeTime lifeTime)
        {
            ServiceType = serviceType;
            ImplementationType = implementationType;
            ConstructorArgTypes = new List<Type>();
            LifeTime = lifeTime;
        }

        public ServiceDescriptor(Type serviceType, Type implementationType, LifeTime lifeTime, object implementation)
            : this(serviceType, implementationType, lifeTime)
        {
            Implementation = implementation;
        }

        public ConstructorInfo GetConstructorInfo()
        {
            if (ConstructorArgTypes.Any())
            {
                return ImplementationType.GetConstructor(ConstructorArgTypes.ToArray());
            }

            ConstructorInfo[] constructorInfos = ImplementationType.GetConstructors();

            if (constructorInfos.Length > 1)
            {
                return null;
            }

            ConstructorInfo constructorInfo = constructorInfos.First();
            return constructorInfo;
        }
    }
}