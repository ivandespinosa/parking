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
    public class DataAccessSqlServerRate : IDataAccessRate
    {
        private readonly Context _context;

        public DataAccessSqlServerRate(Context context)
        {
            _context = context;
        }

        public async Task<List<Rate>> GetRatesAsync()
        {
            return await _context.Rates.ToListAsync().ConfigureAwait(false);
        }

        public async Task<Rate> GetRateVehicleIdAsync(int VehicleId, int StartTime)
        {
            return await _context.Rates.Where(r => r.VehicleId == VehicleId && r.StartTime == StartTime).FirstOrDefaultAsync().ConfigureAwait(false);
        }
    }
}
