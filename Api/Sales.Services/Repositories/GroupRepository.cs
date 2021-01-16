using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sales.Domain.Data;
using Sales.Domain.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sales.Services.Repositories
{
    public interface IGroupRepository
    {
        Task<List<GroupListItem>> ListAsync(GroupType type, CancellationToken cancellationToken);
    }

    public class GroupRepository : IGroupRepository
    {
        private readonly SalesDbContext _context;
        private readonly ILogger<GroupRepository> _logger;

        public GroupRepository(SalesDbContext context,
            ILogger<GroupRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Returns a list of groups matching the provided type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A list of group items</returns>
        public async Task<List<GroupListItem>> ListAsync(GroupType type, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Retrieving all groups of type {type}");

            var query = _context.Groups
                .Where(x => x.Type == type)
                .OrderBy(x => x.Title)
                .Select(g => new GroupListItem
                {
                    GroupId = g.GroupId,
                    Title = g.Title
                });

            return await query.ToListAsync(cancellationToken);
        }
    }

    public class GroupListItem
    {
        public string GroupId { get; set; }
        public string Title { get; set; }
    }
}
