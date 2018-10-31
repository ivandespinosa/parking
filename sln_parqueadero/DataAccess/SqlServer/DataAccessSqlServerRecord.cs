using DataAccess.Contracts;
using Models;
using Models.Dtos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.SqlServer
{
    public class DataAccessSqlServerRecord : IDataAccessRecord
    {
        private readonly Context _context;
        public DataAccessSqlServerRecord(Context context)
        {
            _context = context;
        }

        public async Task<List<RecordDto>> GetRecordsAsync()
        {
            return await _context.Records
                .Select(r => 
                new RecordDto
                {
                    CellNumber = r.Cell.CellNumber,
                    EntryDate = r.EntryDate,
                    Id = r.Id,
                    OutputDate = r.OutputDate,
                    Plate = r.Plate,
                    PriceTimeParking = r.PriceTimeParking,
                    State = r.State,
                    TotalDays = r.TotalDays,
                    TotalHours = r.TotalHours,
                    VehicleDescription = r.Cell.Vehicle.Description
                })
                .OrderBy(r => !r.State)
                .ThenByDescending(r => r.OutputDate)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Record> GetRecordAsync(int Id)
        {
            return await _context.Records.Include("Cell").FirstOrDefaultAsync(x => x.Id == Id).ConfigureAwait(false);
        }

        public async Task<int> OutputVehicleAsync(Record record)
        {
            using(var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    Cell cell = _context.Cells.Find(record.CellId);
                    cell.State = false;
                    _context.Entry(cell).State = EntityState.Modified;

                    _context.Entry(record).State = EntityState.Modified;
                    var countRows = await _context.SaveChangesAsync().ConfigureAwait(false);
                    transaction.Commit();
                    return countRows;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return 0;
                }
            }            
        }

        public async Task<int> RecordVehicleAsync(Record record)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    Cell cell = _context.Cells.Find(record.CellId);
                    cell.State = true;
                    _context.Entry(cell).State = EntityState.Modified;

                    _context.Records.Add(record);
                    var countRows = await _context.SaveChangesAsync().ConfigureAwait(false);
                    transaction.Commit();
                    return countRows;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return 0;
                }
            }            
        }

        public async Task<RecordDto> GetRecordPlateStateAsync(string Plate)
        {
            try
            {               
                var recordDTO = await _context.Records
                    .Where(r => r.Plate == Plate && r.State)
                    .Select(r => 
                    new RecordDto
                    {
                        CellNumber = r.Cell.CellNumber,
                        EntryDate = r.EntryDate,
                        Id = r.Id,
                        OutputDate = r.OutputDate,
                        Plate = r.Plate,
                        PriceTimeParking = r.PriceTimeParking,
                        State = r.State,
                        TotalDays = r.TotalDays,
                        TotalHours = r.TotalHours,
                        VehicleDescription = r.Cell.Vehicle.Description
                    })
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

                return recordDTO;
            }
            catch (Exception)
            {
                return new RecordDto { };
            }
        }
    }
}
