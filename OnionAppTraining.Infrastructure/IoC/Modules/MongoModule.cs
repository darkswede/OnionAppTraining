using Autofac;
using MongoDB.Driver;
using OnionAppTraining.Infrastructure.MongoDB;
using OnionAppTraining.Infrastructure.Repositories;
using System.Reflection;

namespace OnionAppTraining.Infrastructure.IoC.Modules
{
    public class MongoModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register((component, parameter) =>
            {
                var settings = component.Resolve<MongoDBSettings>();

                return new MongoClient(settings.ConnectionString);
            }).SingleInstance();

            builder.Register((component, parameter) =>
            {
                var client = component.Resolve<MongoClient>();
                var settings = component.Resolve<MongoDBSettings>();
                var database = client.GetDatabase(settings.Database);

                return database;
            }).As<IMongoDatabase>();

            var assembly = typeof(RepositoryModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IMongoRepository>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
