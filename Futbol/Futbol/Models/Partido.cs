using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Futbol.Models
{
    [Table("PARTIDO")]
    public class PARTIDO
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey(nameof(EQUIPO))]
        public int EquipoLocalId { get; set; }
        
        [ForeignKey(nameof(EQUIPO))]
        public int EquipoVisitianteId { get; set; }

        [ForeignKey("EquipoLocalId")]
        public virtual EQUIPO EquipoLocal { get; set; }
        
        [ForeignKey("EquipoVisitianteId")]
        public virtual EQUIPO EquipoVisitante { get; set; }

    }
}
