using Models;
using Repository.Persistance.EntityConfigurations;
using System.Data.Entity;

namespace Repository
{
    public class CGVSContext : DbContext
    {
        private static CGVSContext _instance = null;
        private static object syncLock = new object();

        protected CGVSContext():base("Data Source=.;Initial Catalog=CGVS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True")
        {
            Configuration.LazyLoadingEnabled = false;
        }
        protected CGVSContext(string DBCS) : base(DBCS)
        {
            Configuration.LazyLoadingEnabled = false;
        }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Album> Albums { get; set; }

        public static CGVSContext context()
        {
            if (_instance == null)
            {
                lock (syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new CGVSContext();
                    }
                }
            }
            return _instance;
        }
        public static CGVSContext context(string DBCS)
        {
            if (_instance == null)
            {
                lock (syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new CGVSContext(DBCS);
                    }
                }
            }
            return _instance;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ImageConfiguration());
            base.OnModelCreating(modelBuilder);
        }

    }
}
