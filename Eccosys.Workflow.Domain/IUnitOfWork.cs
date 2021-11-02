namespace Eccosys.Workflow.Domain
{
    public interface IUnitOfWork
    {
        ITransaction CreateTransaction();
    }
}
