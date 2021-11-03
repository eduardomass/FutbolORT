using Futbol.BaseDatos;
using Futbol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Futbol.Reglas
{
    public class RNUsuarios
    {
        /// <summary>
        /// Metodo que sirve para realizar un Toogle entre si estaba o no estaba confirmado
        /// </summary>
        /// <param name="_context">Contexto de Base de Datos</param>
        /// <param name="idJugador">Jugador que se intenta cambiar el estado.</param>
      
        public static Usuario ObtenerUsuario(FutbolDbContext _context, string email, string password)
        {

            Usuario usuario = _context.Jugadores.FirstOrDefault(o => o.Email.ToUpper() == email.ToUpper() && password == o.Password);
            if (usuario == null)
                usuario = _context.Administradores.FirstOrDefault(o => o.Email.ToUpper() == email.ToUpper() && password == o.Password);
            return usuario;
        }
       
    }
}
