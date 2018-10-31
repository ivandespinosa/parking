using DataAccess.Contracts;
using DataAccess.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.SqlServer
{
    public class ContextSqlServer : Context
    {
        public ContextSqlServer()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ContextSqlServer, Configuration>());
        }
    }
}
