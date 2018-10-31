using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public interface IDataAccessRate
    {
        Task<List<Rate>> GetRatesAsync();
        Task<Rate> GetRateVehicleIdAsync(int VehicleId, int StartTime);
    }
}
