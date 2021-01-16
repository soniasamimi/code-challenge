
using Microsoft.EntityFrameworkCore;
using Sales.Domain.Enums;

namespace Sales.Domain.Data.Entities
{
    public class Group
    {
        public string GroupId { get; set; }
        public string Title { get; set; }
        public GroupType Type { get; set; }
    }

    public static partial class MappingsExtensions
    {
        public static void MapGroup(this ModelBuilder modelBuilder)
        {
            var model = modelBuilder.Entity<Group>();
            model.HasKey(x => x.GroupId);
        }
    }
}
