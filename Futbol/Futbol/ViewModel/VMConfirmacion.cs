using Futbol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Futbol.ViewModel
{
    public class VMConfirmacion
    {
        public int CantidadLugaresDisponibles { get; set; }

        public Jugador Jugador { get; set; }
        public DateTime FechaPartido { get; set; }
        public bool Confirmado { get; set; }
        public string ConfirmadoString { get
            {
                if (this.Confirmado)
                    return "No jugar";
                else
                    return "Jugar";
            }
        }
    }
}
