using Models;
using Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public interface IDataAccessCell
    {
        Task<List<CellDto>> GetCellsAsync(int VehicleId);
        int CellState(Cell cell);
        int InsertCell(Cell cell);
    }
}
