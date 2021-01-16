using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Sales.Domain.Data.Entities
{
    public class Person
    {
        public Guid PersonId { get; set; }
        public string Name { get; set; }
        public bool Assigned { get; set; }
        public ICollection<PersonGroup> PersonGroups { get; set; }
    }

    public static partial class MappingsExtensions
    {
        public static void MapPerson(this ModelBuilder modelBuilder)
        {
            var model = modelBuilder.Entity<Person>();
            model.HasKey(x => x.PersonId);
            model.HasMany(x => x.PersonGroups)
                .WithOne(x => x.Person)
                .HasForeignKey(x => x.PersonId);
        }
    }
}
