using Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public class Context : DbContext
    {
        protected Context() : base("ContextSqlServer")
        {
            this.Configuration.LazyLoadingEnabled = false;            
        }

        public Context(DbConnection connection) : base(connection, contextOwnsConnection: true)
        {

        }

        public DbSet<Parking> Parkings { get; set; }
        public DbSet<Cell> Cells { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Record> Records { get; set; }
    }
}
