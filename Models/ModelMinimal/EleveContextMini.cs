using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Models.ModelMinimal
{
    internal class EleveContextMini : DbContext, IEleveContextMini
    {
        // Constructeur de la classe EleveContext qui prend en paramètre des options de configuration
        // et les passe à la classe de base DbContext
        public EleveContextMini(DbContextOptions<EleveContextMini> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuration de l'entité Eleve pour définir les relations avec l'entité School
            modelBuilder.Entity<EleveMini>()
                // Un élève a une école
                .HasOne(e => e.Schools)
                // Une école peut avoir plusieurs élèves
                .WithMany(s => s.Eleves)
                // Définition de la clé étrangère SchoolId dans l'entité Eleve
                .HasForeignKey(e => e.SchoolId);
        }

        public DbSet<EleveMini> Eleves { get; set; }
        public DbSet<SchoolMini> Schools { get; set; }
    }
}