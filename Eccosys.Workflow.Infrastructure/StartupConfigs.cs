using Eccosys.Workflow.Common.Config;
using Eccosys.Workflow.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Eccosys.Workflow.Infrastructure
{
    public sealed class StartupConfigs : StartupConfigurations
    {
        public StartupConfigs(IAppConfigs configs, IServiceCollection services) : base(configs, services)
        {

        }

        protected override void ConfigureServices(IAppConfigs configs, IServiceCollection services)
        {
            services.AddDbContext<EntityContext>(options =>
            {
                options
                    //.UseLazyLoadingProxies()
                    .EnableSensitiveDataLogging()
                    .UseNpgsql(configs.WorkflowDbConnectionString)
                    .EnableDetailedErrors(true)
                    .EnableSensitiveDataLogging(true);
            });
        }
    }
}
