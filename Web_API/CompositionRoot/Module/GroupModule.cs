using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompositionRoot.Module
{
    public static class GroupModule
    {
        public static void InjectDependencies(ContainerBuilder builder)
        {
            builder.RegisterModule<BllServiceModule>();
            builder.RegisterModule<DalServiceModule>();

            //var container = builder.Build();

            //return new AutofacServiceProvider(container);
        }
    }
}
