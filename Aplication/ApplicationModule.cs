using Aplication.Pipeline;
using Autofac;

namespace Aplication
{
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = GetType().Assembly;

            // Register Behaviors
            builder.RegisterGeneric(typeof(LoggingBehavior<,>))
                .AsImplementedInterfaces();
            builder.RegisterGeneric(typeof(TimingBehavior<,>))
                .AsImplementedInterfaces();

            // Register Handlers
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Behavior") == false)
                .AsImplementedInterfaces();
        }
    }
}
