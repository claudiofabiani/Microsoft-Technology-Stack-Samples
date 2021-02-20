using Autofac;
using DAL.UnitOfWork;
using DAL.UnitOfWork.Context;
using DAL.UnitOfWork.Repository.Implementation;
using DAL.UnitOfWork.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace CompositionRoot.Module
{
    public class DalServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(x =>
            {
                IConfiguration configuration = x.Resolve<IConfiguration>();
                var optionsBuilder = new DbContextOptionsBuilder<EFContext>();
                optionsBuilder.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")
                    );
                return new EFContext(optionsBuilder.Options);
            }).InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<StudentRepository>().As<IStudentRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().InstancePerLifetimeScope();
        }
    }
}
