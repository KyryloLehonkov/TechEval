using System.Threading.Tasks;

namespace TechEval.Core
{
    public interface ICommandHandler<TCommand, TEntity>
    {
        Task<CommandResult<TEntity>> Execute(TCommand command);
    }
}
