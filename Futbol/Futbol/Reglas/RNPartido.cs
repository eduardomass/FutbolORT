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
