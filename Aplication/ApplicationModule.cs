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
            builder.RegisterGeneric(typeof(TimingBehavior<,>))
                .AsImplementedInterfaces();
            builder.RegisterGeneric(typeof(LoggingBehavior<,>))
                .AsImplementedInterfaces();
            builder.RegisterGeneric(typeof(ValidationBehavior<,>))
                .AsImplementedInterfaces();

            // Register Handlers
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Behavior") == false)
                .AsImplementedInterfaces();
        }
    }
}
