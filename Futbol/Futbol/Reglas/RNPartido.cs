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
        /// <summary>
        /// Metodo que sirve para realizar un Toogle entre si estaba o no estaba confirmado
        /// </summary>
        /// <param name="_context">Contexto de Base de Datos</param>
        /// <param name="idJugador">Jugador que se intenta cambiar el estado.</param>
        public static void ConfirmarODesconfirmarJugador(FutbolDbContext _context,
            int idJugador)
        {
            RNPartido.ConfirmarODesconfirmarJugador(_context, idJugador, true, true);
        }
            public static void ConfirmarODesconfirmarJugador(FutbolDbContext _context, 
            int idJugador,
            bool confirmo, 
            bool toggle = false)
        {
            var jugador = _context.Jugadores.Where(o => o.Id == idJugador).FirstOrDefault();
            var ultimoPartido = RNPartido.ObtenerPartidoProximoAJugar(_context);
            List<int> idsEquiposPosibles = new List<int>();
            idsEquiposPosibles.Add(ultimoPartido.EquipoLocalId);
            idsEquiposPosibles.Add(ultimoPartido.EquipoVisitianteId);

            var listaJugadoresPorEquipos = _context.JugadoresPorEquipos.Where(o => idsEquiposPosibles.Contains(o.EquipoId) &&
                                o.JugadorId == idJugador).ToList();
            bool estabaConfimado = listaJugadoresPorEquipos.Count > 0;

            if (toggle)
                confirmo = !estabaConfimado;

            if (estabaConfimado && !confirmo)
            {
                //Ya estaba confirmado, desconfirma, hay que eliminar
                var jugadorPorEquipoAnteriormenteConfimado = 
                    listaJugadoresPorEquipos.FirstOrDefault();
                _context.JugadoresPorEquipos.Remove(jugadorPorEquipoAnteriormenteConfimado);
                _context.SaveChanges();

            }
            else if (!estabaConfimado && confirmo)
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
        public static Partido ObtenerPartidoProximoAJugar(FutbolDbContext _context)
        {

            var ultimoPartido = _context.Partidos
             .OrderBy(x => x.Fecha)
             .Where(o=>  o.Fecha.Date >= DateTime.Now.Date )
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
