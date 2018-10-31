using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Description { get; set; }

        [ExcludeFromCodeCoverage]
        public virtual ICollection<Rate> Rates { get; set; }
        [ExcludeFromCodeCoverage]
        public virtual ICollection<Cell> Cells { get; set; }
    }
}
