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
        public DbSet<Usuario> Usuarios { get; set; }

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
