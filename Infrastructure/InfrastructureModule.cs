
using Autofac;

namespace Infrastructure
{
    public class InfrastructureModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register DbContext
            builder.Register(c => new DatabaseContext())
                .AsSelf()
                .InstancePerRequest();
        }
    }
}
