using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Sales.Domain.Data.Entities;
using Sales.Domain.Enums;
using System;
using System.IO;
using System.Linq;

namespace Sales.Domain.Data
{
    public class SalesDbContext : DbContext
    {
        public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options)
        {
            //  Predefined groups
            Groups.Add(new Group { GroupId = "A", Title = "Speak Greek", Type = GroupType.Language });
            Groups.Add(new Group { GroupId = "B", Title = "Sports car specialist", Type = GroupType.Speciality });
            Groups.Add(new Group { GroupId = "C", Title = "Family car specialist", Type = GroupType.Speciality });
            Groups.Add(new Group { GroupId = "D", Title = "Tradie vehicle specialist", Type = GroupType.Speciality });

            //  Read the data from the JSON file
            var data = JsonConvert.DeserializeObject<SalesPersonDto[]>(File.ReadAllText("Data.json"));

            //  Convert the JSON object into EF entities
            foreach (var item in data)
            {
                Persons.Add(new Person
                {
                    PersonId = Guid.NewGuid(),  //  Random Person Id
                    Name = item.Name,
                    Assigned = new System.Random().Next() % 2 == 0,    //  Random Assignment
                    PersonGroups = item.Groups.Select(x => new PersonGroup { GroupId = x }).ToList()
                });
            }

            SaveChanges();
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonGroup> PersonGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.MapGroup();
            modelBuilder.MapPerson();
            modelBuilder.MapPersonGroup();
        }

        private class SalesPersonDto
        {
            public string Name { get; set; }
            public string[] Groups { get; set; }
        }
    }
}
