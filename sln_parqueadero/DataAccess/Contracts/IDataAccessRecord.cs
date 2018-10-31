using Models;
using Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public interface IDataAccessRecord
    {
        Task<List<RecordDto>> GetRecordsAsync();
        Task<Record>GetRecordAsync(int Id);
        Task<RecordDto> GetRecordPlateStateAsync(string Plate);
        Task<int> RecordVehicleAsync(Record record);
        Task<int> OutputVehicleAsync(Record record);
    }
}
