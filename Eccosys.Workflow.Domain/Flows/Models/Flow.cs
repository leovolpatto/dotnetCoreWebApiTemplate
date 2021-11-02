using Eccosys.Workflow.Domain.Steps.Models;
using System.Collections.Generic;

namespace Eccosys.Workflow.Domain.Flows.Models
{
    public class Flow : BaseModel
    {
        public string Name { get; set; }

        public List<Step> Steps { get; set; }
    }
}
