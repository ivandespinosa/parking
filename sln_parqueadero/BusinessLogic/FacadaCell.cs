using DataAccess.Contracts;
using Models;
using Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class FacadaCell
    {
        private readonly IDataAccessCell _dataAccessCell;

        public FacadaCell(IDataAccessCell dataAccessCell)
        {
            _dataAccessCell = dataAccessCell;
        }

        public async Task<List<CellDto>> GetCellsAsync(int VehicleId)
        {
            return await _dataAccessCell.GetCellsAsync(VehicleId).ConfigureAwait(false);
        }

        public bool CellState(Cell cell)
        {
            if(cell.Id != 0)
            {
                cell.State = !cell.State;
                bool response = _dataAccessCell.CellState(cell) == 1;
                return response;
            }
            else
            {
                return false;
            }            
        }

        public bool InsertCell(Cell cell)
        {
            return _dataAccessCell.InsertCell(cell) == 1;
        }
    }
}
