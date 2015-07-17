
using System.Data.Entity.ModelConfiguration;

namespace ToDo.Api.Models.EntityConfiguration
{
    public class ToDoConfiguration : EntityTypeConfiguration<Entities.ToDo>
    {
        public ToDoConfiguration()
        {
            ToTable("ToDo");
            HasKey(n => n.ToDoId);
            Property(n => n.Description).HasColumnType("varchar").IsRequired().HasMaxLength(250);
            HasRequired(n => n.User).WithMany(n => n.Doings).HasForeignKey(n => n.UserId);
        }
    }
}
