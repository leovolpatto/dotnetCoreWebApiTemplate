using Autofac;
using Eccosys.Workflow.Common.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Workflow
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Workflow", Version = "v1" });
            });

            new Eccosys.Workflow.Infrastructure.StartupConfigs(new AppConfigs(this.Configuration), services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Workflow v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new Eccosys.Workflow.Common.IoC.IoC(builder, this.Configuration));
            builder.RegisterModule(new Eccosys.Workflow.Domain.IoC(builder, this.Configuration));
            builder.RegisterModule(new Eccosys.Workflow.Application.IoC(builder, this.Configuration));
            builder.RegisterModule(new Eccosys.Workflow.Infrastructure.IoC(builder, this.Configuration));
            builder.RegisterModule(new Eccosys.Workflow.Api.IoC(builder, this.Configuration));
        }
    }
}
