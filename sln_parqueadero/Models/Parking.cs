using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Parking
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string NitRut { get; set; }

        [ExcludeFromCodeCoverage]
        public virtual ICollection<Cell> Cells { get; set; }
    }
}
