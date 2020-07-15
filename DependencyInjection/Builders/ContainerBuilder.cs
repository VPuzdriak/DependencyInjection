using System.Collections.Generic;
using DependencyInjection.Descriptors;

namespace DependencyInjection.Builders
{
    public class ContainerBuilder
    {
        private readonly IList<ServiceDescriptor> _descriptors;

        public ContainerBuilder()
        {
            _descriptors = new List<ServiceDescriptor>();
        }

        public IContainer Build()
        {
            return new Container(_descriptors);
        }

        public ServiceDescriptorBuilder Register<TService>(TService implementation)
        {
            var builder = new ServiceDescriptorBuilder(implementation);
            
            _descriptors.Add(builder.Descriptor);
            
            return builder;
        }

        public ServiceDescriptorBuilder RegisterType<TService>()
        {
            var builder = new ServiceDescriptorBuilder(typeof(TService));
            
            _descriptors.Add(builder.Descriptor);
            
            return builder;
        }
    }
}