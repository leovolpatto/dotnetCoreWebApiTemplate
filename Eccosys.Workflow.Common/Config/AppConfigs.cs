using Microsoft.Extensions.Configuration;

namespace Eccosys.Workflow.Common.Config
{
    public sealed class AppConfigs : IAppConfigs
    {
        private readonly IConfiguration _configuration;
        public AppConfigs(IConfiguration configuration)
        {
            _configuration = configuration;
            this.Parse();
        }
        
        private void Parse()
        {
            this.WorkflowDbConnectionString = _configuration.GetConnectionString("WorkflowDB");
        }

        public string WorkflowDbConnectionString { get; set; }
    }
}
