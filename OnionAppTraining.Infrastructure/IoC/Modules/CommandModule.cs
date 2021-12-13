﻿using Autofac;
using OnionAppTraining.Infrastructure.Commands;
using System.Reflection;

namespace OnionAppTraining.Infrastructure.IoC.Modules
{
    public class CommandModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(ContainerModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<CommandDispatcher>()
                .As<ICommandDispatcher>()
                .InstancePerLifetimeScope();
        }
    }
}