using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using QRest.AspNetCore;

using TechEval.Core;
using TechEval.DataContext;

namespace TechEval.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController: ControllerBase
    {
        private readonly IODataDispatcher _dataDispatcher;

        public TransactionController(IODataDispatcher dataDispatcher)
        {
            _dataDispatcher = dataDispatcher;
        }

        [HttpGet("{query?}")]
        public virtual IActionResult Get(Query query)
        {
            var syncIOFeature = HttpContext.Features.Get<IHttpBodyControlFeature>();
            if (syncIOFeature != null)
            {
                syncIOFeature.AllowSynchronousIO = true;
            }


            var data = _dataDispatcher.Dispatch<Transaction>();
            return query.ToActionResult(data);
        }

    }
}
