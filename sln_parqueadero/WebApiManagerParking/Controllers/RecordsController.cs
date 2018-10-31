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
    public class RecordsController : ApiController
    {
        private readonly FacadaRecord _facadaRecord;

        public RecordsController(FacadaRecord facadaRecord)
        {
            _facadaRecord = facadaRecord;
        }

        public async Task<IEnumerable<RecordDto>> GetRecordsAsync()
        {
            return await _facadaRecord.GetRecordsAsync().ConfigureAwait(false);
        }

        public async Task<RecordDto> GetRecordPlateStateAsync(string Plate)
        {
            return await _facadaRecord.GetRecordPlateStateAsync(Plate).ConfigureAwait(false);
        }

        public async Task<bool> RecordVehicleAsync(Record record)
        {
            return await _facadaRecord.RecordVehicleAsync(record).ConfigureAwait(false);
        }
                
        public async Task<bool> OutputVehicleAsync(int Id)
        {
            var record = await _facadaRecord.GetRecordAsync(Id).ConfigureAwait(false);
            return await _facadaRecord.OutputVehicleAsync(record).ConfigureAwait(false);
        }
    }
}
