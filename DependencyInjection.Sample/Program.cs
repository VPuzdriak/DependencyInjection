using System;
using DependencyInjection.Builders;
using DependencyInjection.Sample.Services.GuidGenerator;
using DependencyInjection.Sample.Services.SomeService;

namespace DependencyInjection.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Sample1();
            Sample2();
            Sample3();
            Sample4();
            Sample5();
            Sample6();
        }

        private static void Sample1()
        {
            Console.WriteLine("--------------Sample 1------------------");

            var builder = new ContainerBuilder();

            builder.Register(new RandomGuidGenerator())
                .SingleInstance();

            IContainer container = builder.Build();

            var guidGenerator1 = container.Resolve<RandomGuidGenerator>();
            Console.WriteLine(guidGenerator1.Guid);

            var guidGenerator2 = container.Resolve<RandomGuidGenerator>();
            Console.WriteLine(guidGenerator2.Guid);

            Console.WriteLine(guidGenerator1.Equals(guidGenerator2));

            Console.WriteLine("----------End of Sample 1---------------");
        }

        private static void Sample2()
        {
            Console.WriteLine("--------------Sample 2------------------");

            var builder = new ContainerBuilder();

            builder.RegisterType<RandomGuidGenerator>()
                .SingleInstance();

            IContainer container = builder.Build();

            var guidGenerator1 = container.Resolve<RandomGuidGenerator>();
            Console.WriteLine(guidGenerator1.Guid);

            var guidGenerator2 = container.Resolve<RandomGuidGenerator>();
            Console.WriteLine(guidGenerator2.Guid);

            Console.WriteLine(guidGenerator1.Equals(guidGenerator2));

            Console.WriteLine("----------End of Sample 2---------------");
        }

        private static void Sample3()
        {
            Console.WriteLine("--------------Sample 3------------------");

            var builder = new ContainerBuilder();

            builder.RegisterType<RandomGuidGenerator>()
                .As<IGuidGenerator>()
                .SingleInstance();

            IContainer container = builder.Build();

            var guidGenerator1 = container.Resolve<IGuidGenerator>();
            Console.WriteLine(guidGenerator1.Guid);

            var guidGenerator2 = container.Resolve<IGuidGenerator>();
            Console.WriteLine(guidGenerator2.Guid);

            Console.WriteLine(guidGenerator1.Equals(guidGenerator2));

            Console.WriteLine("----------End of Sample 3---------------");
        }

        private static void Sample4()
        {
            Console.WriteLine("--------------Sample 4------------------");

            var builder = new ContainerBuilder();

            builder.RegisterType<RandomGuidGenerator>()
                .As<IGuidGenerator>()
                .InstancePerDependency();

            IContainer container = builder.Build();

            var guidGenerator1 = container.Resolve<IGuidGenerator>();
            Console.WriteLine(guidGenerator1.Guid);

            var guidGenerator2 = container.Resolve<IGuidGenerator>();
            Console.WriteLine(guidGenerator2.Guid);

            Console.WriteLine(guidGenerator1.Equals(guidGenerator2));

            Console.WriteLine("----------End of Sample 4---------------");
        }

        private static void Sample5()
        {
            Console.WriteLine("--------------Sample 5------------------");

            var builder = new ContainerBuilder();

            builder.RegisterType<RandomGuidGenerator>()
                .As<IGuidGenerator>()
                .InstancePerDependency();

            builder.RegisterType<SomeService>()
                .As<ISomeService>()
                .UsingConstructor(typeof(IGuidGenerator))
                .InstancePerDependency();

            IContainer container = builder.Build();

            var someService1 = container.Resolve<ISomeService>();
            someService1.PrintGuid();

            var someService2 = container.Resolve<ISomeService>();
            someService2.PrintGuid();

            Console.WriteLine(someService1.Equals(someService2));

            Console.WriteLine("----------End of Sample 5---------------");
        }
        
        private static void Sample6()
        {
            Console.WriteLine("--------------Sample 6------------------");

            var builder = new ContainerBuilder();

            builder.RegisterType<RandomGuidGenerator>()
                .As<IGuidGenerator>()
                .InstancePerDependency();

            builder.RegisterType<SomeService>()
                .UsingConstructor(typeof(IGuidGenerator))
                .As<ISomeService>()
                .InstancePerDependency();

            IContainer container = builder.Build();

            var someService = container.Resolve<ISomeService>();
            someService.PrintGuid();

            Console.WriteLine($"Disposed {someService.Disposed}");
            
            container.Dispose();
            
            Console.WriteLine($"Disposed {someService.Disposed}");

            Console.WriteLine("----------End of Sample 6---------------");
        }
    }
}