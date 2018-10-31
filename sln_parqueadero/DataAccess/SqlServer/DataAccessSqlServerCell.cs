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
    public class DataAccessSqlServerCell : IDataAccessCell
    {
        private readonly Context _context;
        public DataAccessSqlServerCell(Context context)
        {
            _context = context;
        }

        public int CellState(Cell cell)
        {
            _context.Entry(cell).State = EntityState.Modified;
            return _context.SaveChanges();
        }

        public async Task<List<CellDto>> GetCellsAsync(int VehicleId)
        {
            return await _context.Cells
                .Where(c => !c.State && c.VehicleId == VehicleId)
                .Select(c => 
                new CellDto
                {
                    CellNumber = c.CellNumber,
                    Id = c.Id
                })
                .OrderBy(c => c.CellNumber)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public int InsertCell(Cell cell)
        {
            _context.Cells.Add(cell);
            return _context.SaveChanges();
        }
    }
}
