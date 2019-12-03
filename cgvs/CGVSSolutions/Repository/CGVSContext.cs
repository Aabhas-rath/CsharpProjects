using Models;
using Repository.Persistance.EntityConfigurations;
using System.Data.Entity;

namespace Repository
{
    public class CGVSContext : DbContext
    {
        public CGVSContext():base("name = CGVSCS")
        {
            Configuration.LazyLoadingEnabled = false;
        }
        public CGVSContext(string DBCS) : base(DBCS)
        {
            Configuration.LazyLoadingEnabled = false;
        }
        public virtual DbSet<Images> Images { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ImageConfiguration());
            base.OnModelCreating(modelBuilder);
        }

    }
}
