using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Eclaim.Domain.Entities;

namespace System.Eclaim.Infraestructure.Configurations.Context
{
    public class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : IdentityDbContext<IdentityUser>(options)
    {

        public DbSet<Reclamo> Reclamos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reclamo>()
                .Property(e => e.FechaReclamo)
                .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<Reclamo>()
                .Property(e => e.FechaRegistro)
                .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<Reclamo>()
                .Property(e => e.FechaModificacion)
                .HasColumnType("timestamp without time zone");
        }
    }
}
