using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Futbol.Models
{
    //[Table("PARTIDO")]
    public class Partido
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey(nameof(Equipo))]
        public int EquipoLocalId { get; set; }
        
        [ForeignKey(nameof(Equipo))]
        public int EquipoVisitianteId { get; set; }

        [ForeignKey("EquipoLocalId")]
        public virtual Equipo EquipoLocal { get; set; }
        
        [ForeignKey("EquipoVisitianteId")]
        public virtual Equipo EquipoVisitante { get; set; }

        public DateTime Fecha { get; set; }

    }
}
