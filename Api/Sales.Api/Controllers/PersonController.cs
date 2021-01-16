using Microsoft.AspNetCore.Mvc;
using Sales.Services.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Sales.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        /// <summary>
        /// Returns a matched Salesperson
        /// </summary>
        /// <param name="criteria">Search criteria</param>
        /// <param name="cancellationToken">Cancellation token</param>

        [HttpPost("find")]
        public async Task<ActionResult> Find(SalepersonFindCriteria criteria, CancellationToken cancellationToken)
        {
            var result = await _personRepository.FindAsync(criteria, cancellationToken);
            return Ok(result);
        }
    }
}
