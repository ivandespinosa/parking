using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
    public class RecordDto
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime EntryDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? OutputDate { get; set; }
        public int TotalDays { get; set; }
        public int TotalHours { get; set; }
        public decimal PriceTimeParking { get; set; }
        public string Plate { get; set; }
        public bool State { get; set; }
        public string VehicleDescription { get; set; }
        public string CellNumber { get; set; }
    }
}
