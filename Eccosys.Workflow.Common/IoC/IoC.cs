using Autofac;
using Eccosys.Workflow.Common.Config;
using Microsoft.Extensions.Configuration;

namespace Eccosys.Workflow.Common.IoC
{
    public sealed class IoC : ModuleIoCBase
    {
        public IoC(ContainerBuilder builder, IConfiguration configuration) : base(builder, configuration) { }

        protected override void ConfigureIoC(ContainerBuilder builder, IConfiguration configuration)
        {
            builder.RegisterType<AppConfigs>().As<IAppConfigs>();
        }
    }
}
