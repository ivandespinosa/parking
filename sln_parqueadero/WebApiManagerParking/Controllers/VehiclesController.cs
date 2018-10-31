using BusinessLogic;
using Microsoft.Practices.Unity.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Unity;

namespace WebApiManagerParking.Controllers
{
    public class VehiclesController : ApiController
    {
        private readonly FacadaVehicle _facadaVehicle;

        public VehiclesController(FacadaVehicle facadaVehicle)
        {
            _facadaVehicle = facadaVehicle;
        }

        public async Task<IEnumerable<Vehicle>> GetVehiclesAsync()
        {
            return await _facadaVehicle.GetVehiclesAsync().ConfigureAwait(false);
        }

        public async Task<Vehicle> GetVehicleAsync(int Id)
        {
            return await _facadaVehicle.GetVehicle(Id).ConfigureAwait(false);
        }
    }
}
