using MySql.Data.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;

namespace Repository
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    internal class DatabaseContext<T,IT>: DbContext where T : class, IT, new()
    {
        public DbSet<T> Models { get; set; }

        public DatabaseContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
           
        }
        
           
    }

}

