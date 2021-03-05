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
using WebAPI_0.Handler;
using WebAPI_0.Filter;
using WebAPI_0.Middleware.ApiRespondeMiddleware;
using AutoMapper;
using BLL.Dto;
using DAL.Domain;
using DAL.Extension.Domain;
using BLL.Mapping;

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

            services.AddControllers(options =>
                // I filtri permettono di gestire gli errori che avvengono all'interno del codice
                // Per gestire errori non gestiti all'interno del codice (routing o framework) utilizzare l'handler
              options.Filters.Add(new ApiExceptionFilterAttribute())
            );

            // per farlo funzionare se la classe profile sta in un'altro progetto che referenziamo.
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ITypeAdapterFactory, AutomapperTypeAdapterFactory>();
            //services.AddAutoMapper(typeof(Startup).Assembly);


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

            // vedi risposta strutturata api :https://stackoverflow.com/questions/12806386/is-there-any-standard-for-json-api-response-format
                
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

            // I filtri permettono di gestire gli errori che avvengono all'interno del codice
            // Per gestire errori non gestiti all'interno del codice (routing o framework) utilizzare l'handler
            
            app.UseApiResponseWrapperMiddleware();
            app.ConfigureExceptionHandler();



            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    //public class AutomapperProfile : Profile
    //{
    //    public AutomapperProfile()
    //    {
    //        CreateMap<Student, StudentDto>();
    //        CreateMap<Student, StudentListDto>();

    //        //CreateMap<PaginatedEnumerable<Student>, PaginatedEnumerableDto<StudentListDto>>()
    //        //        //.ForMember(p => p.Items, opt => opt.MapFrom(s => s.Items))
    //        //        ;

    //        CreateMap<PaginatedEnumerable, PaginatedEnumerableDto>();
    //        CreateMap<PaginatedEnumerable<Student>, PaginatedEnumerableDto<StudentDto>>();
    //        //       //.ForMember(p => p.Items, opt => opt.MapFrom(s => s.Items))
    //        //       ;

    //    }
    //}
}
