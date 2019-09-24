using System;
using DryIoc;
using DryIoc.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StudentsManager.Api.Configuration;
using StudentsManager.Api.Data;
using StudentsManager.Core.Infrastructure;
using StudentsManager.Services;

namespace StudentsManager.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var environmentConnectionString = Environment.GetEnvironmentVariable("StudentManagerDatabase");

            var connectionString = !string.IsNullOrEmpty(environmentConnectionString)
                ? environmentConnectionString
                : Configuration.GetConnectionString("StudentManagerDatabase");

            services.TryAddSingleton<IMapper, Mapper>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //            services.AddScoped<IAuthService, AuthService>();
            //            services.AddHttpClient<IAuthService, AuthService>();
            //
            services.AddDbContext<ApplicationContext>(option => option.UseSqlServer(connectionString));
                        services.AddScoped<IApplicationContext, ApplicationContext>();
            //
                        services.AddScoped<IStudentService, StudentService>();
            //            services.AddScoped<ICarrierService, CarrierService>();
            //            services.AddScoped<IDriverService, DriverService>();
            //            services.AddScoped<IVehicleService, VehicleService>();
            //            services.AddScoped<IDictionaryService, DictionaryService>();
            //            services.AddScoped<IRouteService, RouteService>();
            //            services.AddScoped<IDocumentService, DocumentService>();
            //
            //            services.AddScoped<ISignInManager, LgkSignInManager>();
            //
            //            services.Configure<FileStorageSettings>(Configuration.GetSection(nameof(FileStorageSettings)));

            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // to register service per Request use Reuse.InCurrentScope
            // adaptedContainer.Register<IMyService, MyService>(Reuse.InCurrentScope)
            return new Container()
                .WithDependencyInjectionAdapter(services)
                .ConfigureServiceProvider<CompositionRoot>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseMvc();

            UpdateDatabase(app);
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
