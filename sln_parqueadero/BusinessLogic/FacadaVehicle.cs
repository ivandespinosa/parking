using DataAccess.Contracts;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class FacadaVehicle
    {
        private readonly IDataAccessVehicle _dataAccessVehicle;

        public FacadaVehicle(IDataAccessVehicle dataAccessVehicle)
        {
            _dataAccessVehicle = dataAccessVehicle;
        }

        public async Task<List<Vehicle>> GetVehiclesAsync()
        {
            return await _dataAccessVehicle.GetVehiclesAsync().ConfigureAwait(false);
        }

        public async Task<Vehicle> GetVehicle(int Id)
        {
            return await _dataAccessVehicle.GetVehicleAsync(Id).ConfigureAwait(false);
        }

        public async Task<bool> InsertVehicleAsync(Vehicle vehicle)
        {
            try
            {
                return await _dataAccessVehicle.InsertVehicle(vehicle).ConfigureAwait(false) == 1;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
