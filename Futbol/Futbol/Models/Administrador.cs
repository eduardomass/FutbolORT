using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Futbol.Models
{
    //[Table("JUGADOR")]
    public class Administrador : Usuario
    {
        public override Rol Rol => Rol.Administrador;
    }
}
