using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Futbol.Models
{
    public class JugadoresPorEquipo
    {
        [Key]
        public int Id { get; set; }
        public int? JugadorId { get; set; }
        public Jugador Jugador { get; set; }

        public int EquipoId { get; set; }
        public Equipo Equipo { get; set; }

    }
}
