using System.Threading.Tasks;

namespace TechEval.Core
{
    public interface ICommandDispatcher
    {
        Task<CommandResult<TEntity>> Dispatch<TParameter, TEntity>(TParameter command);

    }
}
