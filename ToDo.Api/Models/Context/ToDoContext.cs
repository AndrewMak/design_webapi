using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Conventions;
using ToDo.Api.Models.EntityConfiguration;

namespace ToDo.Api.Models.Context
{
    public class ToDoContext : DbContext
    {
        public ToDoContext()
            : base("cnnLocalDb")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Entities.ToDo> ToDo { get; set; }
        public DbSet<Entities.User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region Conventions
            modelBuilder.Conventions.Remove(new ManyToManyCascadeDeleteConvention());
            modelBuilder.Conventions.Remove(new OneToManyCascadeDeleteConvention());
            modelBuilder.Conventions.Remove(new PluralizingTableNameConvention());
            #endregion

            #region Configuration
            modelBuilder.Configurations.Add(new ToDoConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
