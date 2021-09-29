using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Futbol.Models
{
    //[Table("USUARIOS")]
    public abstract class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string Password { get; set; }

        [Display(Name = "em@il")]
        public string Email { get; set; } = "";

        public abstract Rol Rol { get; }
    }
}
