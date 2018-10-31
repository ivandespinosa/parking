using BusinessLogic;
using Models;
using Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApiManagerParking.Controllers
{
    public class CellsController : ApiController
    {
        private readonly FacadaCell _facadaCell;

        public CellsController(FacadaCell facadaCell)
        {
            _facadaCell = facadaCell;
        }

        public async Task<IEnumerable<CellDto>> GetCellsVehicleIdAsync(int VehicleId)
        {
            return await _facadaCell.GetCellsAsync(VehicleId).ConfigureAwait(false);
        }


    }
}
