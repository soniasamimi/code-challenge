using Microsoft.AspNetCore.Mvc;
using Sales.Domain.Enums;
using Sales.Services.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Sales.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;

        public GroupController(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        /// <summary>
        /// Returns a list of Group items
        /// </summary>
        /// <param name="type">Type of the group</param>
        /// <param name="cancellationToken">Cancellation token</param>
        [HttpGet]
        public async Task<ActionResult> List(GroupType type, CancellationToken cancellationToken)
        {
            var result = await _groupRepository.ListAsync(type, cancellationToken);
            return Ok(result);
        }
    }
}
