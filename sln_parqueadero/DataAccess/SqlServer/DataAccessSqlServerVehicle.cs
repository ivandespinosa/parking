using DataAccess.Contracts;
using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.SqlServer
{
    public class DataAccessSqlServerVehicle : IDataAccessVehicle
    {
        private readonly Context _context;

        public DataAccessSqlServerVehicle(Context context)
        {
            _context = context;
        }

        public async Task<Vehicle> GetVehicleAsync(int Id)
        {
            return await _context.Vehicles.FindAsync(Id).ConfigureAwait(false);
        }

        public async Task<List<Vehicle>> GetVehiclesAsync()
        {
            return await _context.Vehicles.ToListAsync().ConfigureAwait(false);
        }

        public async Task<int> InsertVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            return await _context.SaveChangesAsync().ConfigureAwait(false);
        }
                
    }
}
