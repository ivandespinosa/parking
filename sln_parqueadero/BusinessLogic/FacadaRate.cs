using DataAccess.Contracts;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class FacadaRate
    {
        private readonly IDataAccessRate _dataAccessRate;

        public FacadaRate(IDataAccessRate dataAccessRate)
        {
            _dataAccessRate = dataAccessRate;
        }

        public async Task<List<Rate>> GetRatesAsync()
        {
            return await _dataAccessRate.GetRatesAsync().ConfigureAwait(false);
        }

        public async Task<Rate> GetRateVehicleIdAsync(int VehicleId, int Hour)
        {
            return await _dataAccessRate.GetRateVehicleIdAsync(VehicleId, Hour).ConfigureAwait(false);
        }
    }
}
