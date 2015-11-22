using TORNEOS.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using Autofac;
using FNHMVC.CommandProcessor.Dispatcher;
using TORNEOS.Data.Repositories;
using TORNEOS.Data.Infrastructure;
using TORNEOS.Model.Commands;
using FNHMVC.Core.Common;
using FNHMVC.CommandProcessor.Command;
using System.Reflection;
using NHibernate;
using FluentNHibernate.Cfg;
using AutoMapper;

namespace TORNEOS.Test
{

    [TestClass()]
    public class TorneoTest
    {
        private TestContext testContextInstance;
        private IContainer container;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        public TorneoTest()
        {
            var builder = new ContainerBuilder();

            builder.Register(c => TORNEOS.Data.Infrastructure.ConnectionHelper.BuildSessionFactory("TORNEOSMVCContainer")).As<ISessionFactory>().SingleInstance();
            builder.Register(c => c.Resolve<ISessionFactory>().OpenSession()).InstancePerLifetimeScope();

            builder.RegisterType<DefaultCommandBus>().SingleInstance();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(IRepository<Torneo>).Assembly).Where(t => t.Name.EndsWith("TorneoRepository")).AsImplementedInterfaces().InstancePerLifetimeScope();

            var services = Assembly.Load("TORNEOS.Domain");
            builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(ICommandHandler<>)).InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(IValidationHandler<>)).InstancePerLifetimeScope();

            builder.Register(c => new ConfigurationStore(new TypeMapFactory(), AutoMapper.Mappers.MapperRegistry.Mappers)).AsImplementedInterfaces().SingleInstance();
            builder.Register(c => Mapper.Engine).As<IMappingEngine>().SingleInstance();
            builder.RegisterType<TypeMapFactory>().As<ITypeMapFactory>().SingleInstance();

            container = builder.Build();

            AutoMapperConfiguration.Configure();
        }

        [TestMethod()]
        public void TorneoCreateTest()
        {
            using (var lifetime = container.BeginLifetimeScope())
            {
                ITorneoRepository torneoRepository = lifetime.Resolve<ITorneoRepository>();
                DefaultCommandBus commandBus = lifetime.Resolve<DefaultCommandBus>();
                IMappingEngine mapper = lifetime.Resolve<IMappingEngine>();

                Torneo torneo = new Torneo()
                {
                    Nombre = "Test Torneo",
                    //Description = "This is a test torneo"
                };

                CreateOrUpdateTorneoCommand command = mapper.Map<CreateOrUpdateTorneoCommand>(torneo);
                IValidationHandler<CreateOrUpdateTorneoCommand> validationHandler = lifetime.Resolve<IValidationHandler<CreateOrUpdateTorneoCommand>>();
                IEnumerable<ValidationResult> validations = commandBus.Validate(command, validationHandler);
                foreach (var val in validations)
                {
                    Assert.IsNull(val, "Error: Torneo creation did not validate " + val.Message);
                }
                ICommandHandler<CreateOrUpdateTorneoCommand> commnadHandler = lifetime.Resolve<ICommandHandler<CreateOrUpdateTorneoCommand>>();
                ICommandResult result = commandBus.Submit(command, commnadHandler);
                Assert.IsNotNull(result, "Error: Torneo was not created by commandBus");
                Assert.IsTrue(result.Success, "Error: Torneo was not created by commandBus");
            }
        }

        [TestMethod()]
        public void TorneoGetTest()
        {
            using (var lifetime = container.BeginLifetimeScope())
            {
                ITorneoRepository torneoRepository = lifetime.Resolve<ITorneoRepository>();

                Torneo torneo = torneoRepository.Get(c => c.Nombre == "Test Torneo");
                Assert.IsNotNull(torneo, "Error: Torneo was now found.");
            }
        }

        [TestMethod()]
        public void TorneoUpdateTest()
        {
            Torneo torneo;

            using (var lifetime = container.BeginLifetimeScope())
            {
                ITorneoRepository torneoRepository = lifetime.Resolve<ITorneoRepository>();
                DefaultCommandBus commandBus = lifetime.Resolve<DefaultCommandBus>();
                IMappingEngine mapper = lifetime.Resolve<IMappingEngine>();

                torneo = torneoRepository.Get(c => c.Nombre == "Test Torneo");
                Assert.IsNotNull(torneo, "Error: Torneo was now found.");

                torneo.Nombre = "Updated Test Torneo";

                CreateOrUpdateTorneoCommand command = mapper.Map<CreateOrUpdateTorneoCommand>(torneo);
                IValidationHandler<CreateOrUpdateTorneoCommand> validationHandler = lifetime.Resolve<IValidationHandler<CreateOrUpdateTorneoCommand>>();
                IEnumerable<ValidationResult> validations = commandBus.Validate(command, validationHandler);

                foreach (var val in validations)
                {
                    Assert.IsNull(val, "Error: Torneo creation did not validate " + val.Message);
                }
            }

            using (var lifetime = container.BeginLifetimeScope())
            {
                DefaultCommandBus commandBus = lifetime.Resolve<DefaultCommandBus>();
                IMappingEngine mapper = lifetime.Resolve<IMappingEngine>();

                CreateOrUpdateTorneoCommand command = mapper.Map<CreateOrUpdateTorneoCommand>(torneo);

                ICommandHandler<CreateOrUpdateTorneoCommand> commnadHandler = lifetime.Resolve<ICommandHandler<CreateOrUpdateTorneoCommand>>();
                ICommandResult result = commandBus.Submit(command, commnadHandler);
                Assert.IsNotNull(result, "Error: Torneo was not updated by CommandBus");
                Assert.IsTrue(result.Success, "Error: Provincia was not updated by CommandBus");
            }
        }

        [TestMethod()]
        public void TorneoDeleteTest()
        {
            using (var lifetime = container.BeginLifetimeScope())
            {
                ITorneoRepository torneoRepository = lifetime.Resolve<ITorneoRepository>();
                DefaultCommandBus commandBus = lifetime.Resolve<DefaultCommandBus>();
                IMappingEngine mapper = lifetime.Resolve<IMappingEngine>();

                Torneo torneo = torneoRepository.Get(c => c.Nombre == "Updated Test Torneo");
                Assert.IsNotNull(torneo, "Error: Torneo was now found.");

                DeleteTorneoCommand command = mapper.Map<DeleteTorneoCommand>(torneo);
                ICommandHandler<DeleteTorneoCommand> commnadHandler = lifetime.Resolve<ICommandHandler<DeleteTorneoCommand>>();
                ICommandResult result = commandBus.Submit(command, commnadHandler);
                Assert.IsNotNull(result, "Error: Torneo was not deleted by CommandBus");
                Assert.IsTrue(result.Success, "Error: Torneo was not deleted by CommandBus");
            }
        }
    }
}
