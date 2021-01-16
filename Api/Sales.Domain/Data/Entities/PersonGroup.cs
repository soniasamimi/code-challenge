using Microsoft.EntityFrameworkCore;
using System;

namespace Sales.Domain.Data.Entities
{
    public class PersonGroup
    {
        public Guid PersonId { get; set; }
        public string GroupId { get; set; }
        public Person Person { get; set; }
    }

    public static partial class MappingsExtensions
    {
        public static void MapPersonGroup(this ModelBuilder modelBuilder)
        {
            var model = modelBuilder.Entity<PersonGroup>();
            model.HasKey(x => new { x.PersonId, x.GroupId });
        }
    }
}
