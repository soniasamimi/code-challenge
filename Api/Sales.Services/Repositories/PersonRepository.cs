using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sales.Domain.Data;
using Sales.Domain.Model;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sales.Services.Repositories
{
    public interface IPersonRepository
    {
        Task<Result<string>> FindAsync(SalepersonFindCriteria criteria, CancellationToken cancellationToken);
    }

    public class PersonRepository : IPersonRepository
    {
        private readonly SalesDbContext _context;
        private readonly ILogger<PersonRepository> _logger;

        public PersonRepository(SalesDbContext context,
            ILogger<PersonRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// This method tries to match a salesperson to the provided criteria
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>An object of type Result<string> conaining the Name of the matched salesperson or null and validation errors</returns>
        public async Task<Result<string>> FindAsync(SalepersonFindCriteria criteria, CancellationToken cancellationToken)
        {
            var result = new Result<string>();

            if (criteria.Language == 0)
            {
                result.Errors.Add("Language is required");
            }

            if (result.Success)
            {
                _logger.LogInformation("Executing person match");

                var query = _context.Persons
                    .Where(x => !x.Assigned)
                    .Where(x =>
                        //  Specific Speciality
                        (criteria.Speciality != null &&
                            (
                                //  Speaking Greek
                                (criteria.Language == Language.SpeakingGreek && x.PersonGroups.Any(g => g.GroupId == "A") && x.PersonGroups.Any(g => g.GroupId == criteria.Speciality)) ||
                                //  Not Speaking Greek
                                (criteria.Language == Language.NotSpeakingGreek && x.PersonGroups.All(g => g.GroupId != "A") && x.PersonGroups.Any(g => g.GroupId == criteria.Speciality)) ||
                                //  Any Language
                                (criteria.Language == Language.Any && x.PersonGroups.Any(g => g.GroupId == criteria.Speciality))
                            )
                        ) ||
                        //  Any Speciality
                        (criteria.Speciality == null &&
                            (
                                //  Speaking Greek
                                (criteria.Language == Language.SpeakingGreek && x.PersonGroups.Any(g => g.GroupId == "A")) ||
                                //  Not Speaking Greek
                                (criteria.Language == Language.NotSpeakingGreek && x.PersonGroups.All(g => g.GroupId != "A"))
                            )
                        )
                    )
                    .Select(p => new
                    {
                        Id = Guid.NewGuid(),
                        p.Name
                    })
                    .OrderBy(x => x.Id)
                    .Select(x => x.Name);

                result.Value = await query.FirstOrDefaultAsync(cancellationToken);

                if (result.Value == null)
                {
                    //  If nothing matched for Speaking Greek then find someone with the provided speciality regardless of the language
                    if (criteria.Speciality != null && criteria.Language == Language.SpeakingGreek)
                    {
                        var list = await _context.Persons
                            .Where(x => !x.Assigned)
                            .Where(x => x.PersonGroups.Any(g => g.GroupId == criteria.Speciality))
                            .Select(p => new
                            {
                                Id = Guid.NewGuid(),
                                p.Name
                            })
                            .OrderBy(x => x.Id)
                            .Select(x => x.Name)
                            .ToListAsync(cancellationToken);

                        result.Value = list.FirstOrDefault();
                    }

                    //  If nothing matched then find a random one
                    if (result.Value == null)
                    {
                        var list = await _context.Persons
                            .Where(x => !x.Assigned)
                            .Select(p => new
                            {
                                Id = Guid.NewGuid(),
                                p.Name
                            })
                            .OrderBy(x => x.Id)
                            .Select(x => x.Name)
                            .ToListAsync(cancellationToken);

                        result.Value = list.FirstOrDefault();
                    }
                }
            }

            return result;
        }
    }

    public class SalepersonFindCriteria
    {
        public Language Language { get; set; }
        public string Speciality { get; set; }
    }

    public enum Language
    {
        Any = 1,
        SpeakingGreek = 2,
        NotSpeakingGreek = 3
    }
}
