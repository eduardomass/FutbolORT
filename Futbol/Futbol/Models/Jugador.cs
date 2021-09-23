using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Futbol.Models
{
    [Table("JUGADOR")]
    public class JUGADOR
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }

        [Required]
        public string Email { get; set; }

        public string Foto { get; set; }

        public bool Activo { get; set; }
    }
}
