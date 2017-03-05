using System.Collections.Generic;
using Api.Controllers;
using Aplication;
using Autofac;
using Autofac.Integration.WebApi;
using MediatR;

namespace Api
{
    public static class AutofacConfiguration
    {
        public static void Configure(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(typeof(CinemasController).Assembly);

            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly)
                .AsImplementedInterfaces();
            builder.Register<SingleInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t =>
                {
                    object o;
                    return c.TryResolve(t, out o) ? o : null;
                };
            });
            builder.Register<MultiInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
            });

            builder.RegisterModule<ApplicationModule>();
        }
    }
}