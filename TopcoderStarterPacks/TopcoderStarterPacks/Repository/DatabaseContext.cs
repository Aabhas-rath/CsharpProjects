using MySql.Data.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;

namespace Repository
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class DatabaseContext<T,IT>: DbContext where T : class, IT, new()
    {
        public DbSet<T> Models { get; set; }

        public DatabaseContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
           
        }
        
           
    }

}

