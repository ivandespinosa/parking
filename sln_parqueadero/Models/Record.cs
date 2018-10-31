using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Record
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
        public int Displacement { get; set; }
        public bool State { get; set; }
        
        
        public int CellId { get; set; }
        [ExcludeFromCodeCoverage]
        public virtual Cell Cell { get; set; }
    }
}
