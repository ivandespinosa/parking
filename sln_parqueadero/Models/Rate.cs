using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Rate
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public decimal Price { get; set; }


        public int VehicleId { get; set; }

        [ExcludeFromCodeCoverage]
        public virtual Vehicle Vehicle { get; set; }
    }




}
