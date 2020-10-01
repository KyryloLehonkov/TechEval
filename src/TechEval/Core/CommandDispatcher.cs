using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace TechEval.Core
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _resolver;
        private readonly ILogger<CommandDispatcher> _log;

        public CommandDispatcher(IServiceProvider resolver, ILogger<CommandDispatcher> logger)
        {
            _resolver = resolver;
            _log = logger;
        }
        public async Task<CommandResult<TEntity>> Dispatch<TParameter, TEntity>(TParameter command)
        {
            var type2Search = typeof(ICommandHandler<,>)
                .MakeGenericType(command.GetType(), typeof(TEntity));

            var handler = (ICommandHandler<TParameter, TEntity>)_resolver.GetService(type2Search);
            if (handler == null)
            {
                var errMessage = $"Command handler for {typeof(TParameter)} command not registered";
                _log.LogError(errMessage);
                throw new NullReferenceException(errMessage);
            }

            try
            {
                _log.LogDebug($"Executing command handler for {typeof(TParameter)}");
                return await handler.Execute(command);
            }
            catch (Exception ex)
            {
                var errMessage = $"Error occured during execution of command handler for {typeof(TParameter)}";
                _log.LogError(ex, errMessage);
                throw;
            }
        }
    }
}
