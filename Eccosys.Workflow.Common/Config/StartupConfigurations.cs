using Microsoft.Extensions.DependencyInjection;

namespace Eccosys.Workflow.Common.Config
{
    public abstract class StartupConfigurations
    {
        public StartupConfigurations(IAppConfigs configs, IServiceCollection services)
        {
            this.ConfigureServices(configs, services);
        }

        protected abstract void ConfigureServices(IAppConfigs configs, IServiceCollection services);
    }
}
