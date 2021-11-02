using Autofac;
using Microsoft.Extensions.Configuration;

namespace Eccosys.Workflow.Common.IoC
{
    public abstract class ModuleIoCBase : Module
    {
        public ModuleIoCBase(ContainerBuilder builder, IConfiguration configuration)
        {
            this.ConfigureIoC(builder, configuration);
        }

        protected abstract void ConfigureIoC(ContainerBuilder builder, IConfiguration configuration);
    }
}
