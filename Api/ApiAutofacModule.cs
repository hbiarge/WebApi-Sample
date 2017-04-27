using System.Collections.Generic;
using Api.Controllers.Administration;
using Aplication;
using Autofac;
using Autofac.Integration.WebApi;
using MediatR;

namespace Api
{
    public class ApiAutofacModule: Module
    {
        public bool SampleConfiguration { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(typeof(FilmsController).Assembly);

            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly)
                .AsImplementedInterfaces();

            builder.Register<SingleInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.TryResolve(t, out object o) ? o : null;
            });

            builder.Register<MultiInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
            });

            if (SampleConfiguration)
            {
                // Add conditional registrations based in some configuration
            }

            builder.RegisterModule<ApplicationAutofacModule>();
        }
    }
}