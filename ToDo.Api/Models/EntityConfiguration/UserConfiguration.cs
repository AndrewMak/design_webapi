
using System.Data.Entity.ModelConfiguration;

namespace ToDo.Api.Models.EntityConfiguration
{
    public class UserConfiguration : EntityTypeConfiguration<Entities.User>
    {
        public UserConfiguration()
        {
            ToTable("User");
            HasKey(n => n.UserId);
            Property(n => n.Name).IsRequired().HasColumnType("varchar").HasMaxLength(250);
            Property(n => n.Email).IsRequired().HasColumnType("varchar").HasMaxLength(250);
            HasMany(n => n.Doings).WithRequired(n => n.User).HasForeignKey(n => n.UserId);
        }
    }
}
