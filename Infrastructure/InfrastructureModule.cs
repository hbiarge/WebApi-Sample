
using Autofac;

namespace Infrastructure
{
    public class InfrastructureModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Manually register DbContext
            builder.Register(c => new DatabaseContext())
                .AsSelf()
                .InstancePerRequest();

            // Scan assembly for other registrations
            var assembly = GetType().Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .AsImplementedInterfaces();
        }
    }
}
