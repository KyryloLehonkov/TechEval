using System.Linq;

namespace TechEval.Core
{
    public interface IODataDispatcher
    {
        IQueryable<T> Dispatch<T>() where T : class;
    }
}
