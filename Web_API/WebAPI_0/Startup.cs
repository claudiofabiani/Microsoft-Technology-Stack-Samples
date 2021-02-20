using Autofac;
using DAL.UnitOfWork;
using DAL.UnitOfWork.Context;
using DAL.UnitOfWork.Repository.Implementation;
using DAL.UnitOfWork.Repository.Interface;
using CompositionRoot.Module;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WebAPI_0
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            
            /*
                usare entity framework
                metti retry policy si database
                fare il pattern unit of work con ef crea il DAL (implementa specification, paging e ordering)
                fare un livello di business che usa il UoW pattern 
                l'app user� il layer di business
                cos� ho tre layer

                il dato che ho tre livelli usare il dependency resolver (se ritrovi come avevi fatto)
                
                metti gestion centralizzata di log
                metti risposta strutturata dell�api
                usa automapper con dto

                poi metti swagger
                
             */

            //services.AddDbContext<EFContext>(options =>
            //{
            //options.UseSqlServer(
            //    Configuration.GetConnectionString("DefaultConnection"),
            //    sqlOptions => sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name)
            //    );                
            //},
            //ServiceLifetime.Scoped);

            ////services.AddScoped(typeof(GenericRepository<>));
            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //services.AddScoped<IStudentRepository, StudentRepository>();
            //services.AddScoped<UnitOfWork>();


            
        }

        // https://autofaccn.readthedocs.io/en/latest/integration/aspnetcore.html#asp-net-core-3-0-and-generic-hosting
        public void ConfigureContainer(ContainerBuilder builder)
        {
            GroupModule.InjectDependencies(builder);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
