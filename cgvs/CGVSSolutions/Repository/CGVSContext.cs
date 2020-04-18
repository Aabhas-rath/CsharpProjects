﻿using Microsoft.AspNet.Identity.EntityFramework;
using Models;
using Repository.Persistance.EntityConfigurations;
using System.Data.Entity;

namespace Repository
{
    public class CGVSContext : IdentityDbContext<Repository.Users.ApplicationUser>
    {
        public CGVSContext():base("Data Source=.;Initial Catalog=CGVS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True")
        {
            Configuration.LazyLoadingEnabled = false;
        }
        public CGVSContext(string DBCS) : base(DBCS)
        {
            Configuration.LazyLoadingEnabled = false;
        }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Album> Albums { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ImageConfiguration());
            modelBuilder.Configurations.Add(new AlbumConfigurations());
            base.OnModelCreating(modelBuilder);
        }

    }
}
