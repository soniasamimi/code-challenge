using Sales.Domain.Data.Entities;
using Sales.Domain.Enums;
using Sales.Services.Repositories;
using System;
using System.Collections.Generic;
using Xunit;

namespace Sales.Services.Tests.Repositories
{
    public class PersonRepositoryTests : TestBase<PersonRepository>
    {
        [Theory]
        [InlineData((Language)0, null, false)]
        [InlineData(Language.Any, null, false)]
        [InlineData(Language.SpeakingGreek, null, false)]
        [InlineData(Language.Any, "B", false)]
        [InlineData(Language.SpeakingGreek, "B", false)]
        [InlineData(Language.Any, null, true)]
        [InlineData(Language.SpeakingGreek, null, true)]
        [InlineData(Language.Any, "B", true)]
        [InlineData(Language.SpeakingGreek, "B", true)]
        public async void FindAsync(Language language, string speciality, bool assigned)
        {
            //  Arrange
            Context.Groups.Add(new Group { GroupId = "A", Title = "Speaking Greek", Type = GroupType.Language });
            Context.Groups.Add(new Group { GroupId = "B", Title = " Sports car specialist", Type = GroupType.Speciality });
            Context.Groups.Add(new Group { GroupId = "C", Title = "Family car specialist", Type = GroupType.Speciality });
            Context.Groups.Add(new Group { GroupId = "D", Title = "Tradie vehicle specialist", Type = GroupType.Speciality });

            Context.Persons.Add(new Person
            {
                PersonId = Guid.NewGuid(),
                Name = "Thomas Crane",
                Assigned = assigned,
                PersonGroups = new List<PersonGroup>
                {
                    new PersonGroup
                    {
                        GroupId = "A",
                    },
                    new PersonGroup
                    {
                        GroupId = "B",
                    }
                }
            });

            Context.SaveChanges();

            //  Act
            var result = await Service.FindAsync(new SalepersonFindCriteria
            {
                Language = language,
                Speciality = speciality
            }, default);

            //  Assert
            if (language == 0)
            {
                Assert.False(result.Success);
                Assert.Equal("Language is required", result.Errors[0]);
            }
            else
            {
                Assert.True(result.Success);
                if (assigned)
                {
                    Assert.Null(result.Value);
                }
                else
                {
                    if (speciality == null)
                    {
                        if (language == Language.Any)
                        {
                            Assert.Equal("Thomas Crane", result.Value);
                        }
                        else if (language == Language.SpeakingGreek)
                        {
                            Assert.Equal("Thomas Crane", result.Value);
                        }
                    }
                    else
                    {
                        if (language == Language.Any)
                        {
                            Assert.Equal("Thomas Crane", result.Value);
                        }
                        else if (language == Language.SpeakingGreek)
                        {
                            Assert.Equal("Thomas Crane", result.Value);
                        }
                    }
                }
            }
        }
    }
}
