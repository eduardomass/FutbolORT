using Futbol.BaseDatos;
using Futbol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Futbol.Reglas
{
    public class RNPartido
    {
        public static void ConfirmarODesconfirmarJugador(FutbolDbContext _context, int idJugador)
        {
            var jugador = _context.Jugadores.Where(o => o.Id == idJugador).FirstOrDefault();
            var ultimoPartido = RNPartido.ObtenerUltimoPartido(_context);
            List<int> idsEquiposPosibles = new List<int>();
            idsEquiposPosibles.Add(ultimoPartido.EquipoLocalId);
            idsEquiposPosibles.Add(ultimoPartido.EquipoVisitianteId);

            var listaJugadoresPorEquipos = _context.JugadoresPorEquipos.Where(o => idsEquiposPosibles.Contains(o.EquipoId) &&
                                o.JugadorId == idJugador).ToList();

            if (listaJugadoresPorEquipos.Count > 0)
            {
                //Ya estaba confirmado, desconfirma, hay que eliminar
            }
            else
            {
                //Falta ver a que equipo va, deberia hacer una regla de negocios!
                _context.JugadoresPorEquipos.Add(new JugadoresPorEquipo()
                {
                    EquipoId = ultimoPartido.EquipoLocalId,
                    JugadorId = idJugador
                });
                _context.SaveChanges();
            }
        }
        public static Partido ObtenerUltimoPartido(FutbolDbContext _context)
        {

            var ultimoPartido = _context.Partidos
             .OrderByDescending(x => x.Fecha)
             .FirstOrDefault();
           

            return ultimoPartido;
        }
        public static void AgregarPartido(FutbolDbContext dbContext, DateTime fecha)
        {
            dbContext.Database.BeginTransaction();

            Equipo equipoUno = new Equipo();
            Equipo equipoDos = new Equipo();
            dbContext.Equipos.Add(equipoUno);
            dbContext.Equipos.Add(equipoDos);

            Partido partido = new Partido();
            partido.Fecha = fecha;
            partido.EquipoLocal = equipoUno;
            partido.EquipoVisitante = equipoDos;

            dbContext.Partidos.Add(partido);
            for (int i = 0; i < 5; i++)
            {
                JugadoresPorEquipo jugadorNuevo = new JugadoresPorEquipo();
                jugadorNuevo.Equipo = equipoUno;
                JugadoresPorEquipo jugadorNuevoDos = new JugadoresPorEquipo();
                jugadorNuevoDos.Equipo = equipoDos;
                
                dbContext.JugadoresPorEquipos.Add(jugadorNuevo);
                dbContext.JugadoresPorEquipos.Add(jugadorNuevoDos);

            }
            dbContext.SaveChanges();
            dbContext.Database.CommitTransaction();

        }
    }
}
