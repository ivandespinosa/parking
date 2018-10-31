using DataAccess.Contracts;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.SqlServer
{
    public class DataAccessSqlServerParking : IDataAccessParking
    {
        private readonly Context _context;

        public DataAccessSqlServerParking(Context context)
        {
            _context = context;
        }

        public Parking GetParking()
        {
            return _context.Parkings.FirstOrDefault();
        }

        public int InsertParking(Parking parking)
        {
            _context.Parkings.Add(parking);
            return _context.SaveChanges();
        }
    }
}
