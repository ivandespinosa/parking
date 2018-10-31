using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Cell
    {
        public int Id { get; set; }
        public string CellNumber { get; set; }
        public bool State { get; set; }
        public bool Active { get; set; }
        public int ParkingId { get; set; }
        public int VehicleId { get; set; }

        [ExcludeFromCodeCoverage]
        public virtual ICollection<Record> Records { get; set; }
        [ExcludeFromCodeCoverage]
        public virtual Parking Parking { get; set; }
        [ExcludeFromCodeCoverage]
        public virtual Vehicle Vehicle { get; set; }
    }
}
