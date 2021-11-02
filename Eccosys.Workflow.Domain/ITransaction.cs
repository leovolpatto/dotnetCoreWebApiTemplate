using System;

namespace Eccosys.Workflow.Domain
{
    public interface ITransaction : IDisposable
    {
        public bool Active { get; }
        public void Complete();
        public void CompleteAsync();
        public void Rollback();
    }
}
