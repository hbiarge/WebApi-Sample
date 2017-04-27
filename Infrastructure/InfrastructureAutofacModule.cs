
using Autofac;
using Domain;

namespace Infrastructure
{
    public class InfrastructureAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Manually register DbContext
            builder.Register(c => new DatabaseContext())
                .AsSelf()
                .As<IUnitOfWork>()
                .InstancePerRequest();

            // Scan assembly for other registrations
            var assembly = GetType().Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .AsImplementedInterfaces();
        }
    }
}
