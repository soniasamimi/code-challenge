using Sales.Domain.Data.Entities;
using Sales.Domain.Enums;
using Sales.Services.Repositories;
using Xunit;

namespace Sales.Services.Tests.Repositories
{
    public class GroupRepositoryTests : TestBase<GroupRepository>
    {
        [Fact]
        public async void ListAsync()
        {
            //  Arrange
            Context.Groups.Add(new Group { GroupId = "A", Title = "Speciality 2", Type = GroupType.Speciality });
            Context.Groups.Add(new Group { GroupId = "B", Title = "English", Type = GroupType.Language });
            Context.Groups.Add(new Group { GroupId = "C", Title = "Speciality 1", Type = GroupType.Speciality });
            Context.SaveChanges();

            //  Act
            var result = await Service.ListAsync(GroupType.Speciality, default);

            //  Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("C", result[0].GroupId);
            Assert.Equal("A", result[1].GroupId);
        }
    }
}
