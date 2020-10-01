using System.Linq;
using TechEval.DataContext;

namespace TechEval.Core
{
    public class ODataDispatcher : IODataDispatcher
    {
        private readonly Db _ctx;

        public ODataDispatcher(Db ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<T> Dispatch<T>() where T : class
        {
            return _ctx.Set<T>();
        }
    }

}
