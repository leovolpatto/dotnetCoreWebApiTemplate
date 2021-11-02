using Eccosys.Workflow.Domain.Steps.Models;

namespace Eccosys.Workflow.Domain.Flows.Models
{
    public class FlowStep : BaseModel
    {
        public int StepId { get; set; }
        public int PreviousStepId { get; set; }
        public int NextStepId { get; set; }
        public Step Step { get; set; }
        public Step PreviousStep { get; set; }
        public Step NextStep { get; set; }
        public FlowStepType StepType { get; set; }
    }

    public enum FlowStepType
    {
        /// <summary>
        /// Every flow must start at a starter step
        /// </summary>
        STARTER = 0,

        /// <summary>
        /// Every flow must end at a finisher step
        /// </summary>
        FINISHER = 1,

        /// <summary>
        /// No flow switch will be supported
        /// </summary>
        REGULAR = 2,

        /// <summary>
        /// Indicates that a decision should be made based on step configs
        /// </summary>
        DECISION = 3
    }
}
