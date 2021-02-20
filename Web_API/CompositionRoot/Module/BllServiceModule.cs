using Autofac;
using BLL.Manager.Implementation;
using BLL.Manager.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompositionRoot.Module
{
    public class BllServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<StudentManager>().As<IStudentManager>().InstancePerLifetimeScope();
        }
    }
}
