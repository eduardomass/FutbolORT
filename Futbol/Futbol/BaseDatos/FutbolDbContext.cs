using Futbol.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Futbol.BaseDatos
{
    public class FutbolDbContext : DbContext
    {
        public FutbolDbContext(DbContextOptions opciones) : base(opciones)
        {

        }
        public DbSet<Jugador> Jugadores { get; set; }
        public DbSet<Administrador> Administradores { get; set; }

        public DbSet<Equipo> Equipos { get; set; }

        public DbSet<JugadoresPorEquipo> JugadoresPorEquipos { get; set; }

        public DbSet<Partido> Partidos { get; set; }

        //add-migration Migracion_1
        //udpate-database

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<PARTIDO>()
        //                .Hasre(m => m.HomeTeam)
        //                .WithMany(t => t.HomeMatches)
        //                .HasForeignKey(m => m.HomeTeamId)
        //                .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<PARTIDO>()
        //                .HasRequired(m => m.GuestTeam)
        //                .WithMany(t => t.AwayMatches)
        //                .HasForeignKey(m => m.GuestTeamId)
        //                .WillCascadeOnDelete(false);
        //}
    }
}
