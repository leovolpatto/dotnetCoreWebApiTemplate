using Autofac;
using Eccosys.Workflow.Common.IoC;
using Microsoft.Extensions.Configuration;
using System;

namespace Eccosys.Workflow.Application
{
    public sealed class IoC : ModuleIoCBase
    {
        public IoC(ContainerBuilder builder, IConfiguration configuration) : base(builder, configuration) { }

        protected override void ConfigureIoC(ContainerBuilder builder, IConfiguration configuration)
        {
            
        }
    }
}
