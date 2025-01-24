using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Models.ModelAPI
{
    public class EleveContextAPI : DbContext
    {
        public EleveContextAPI(DbContextOptions<EleveContextAPI> options)
            : base(options)
        {
            // Le constructeur est vide mais peut être utilisé pour initialiser des ressources
        }

        // Propriété DbSet qui représente la collection d'objets Eleve dans la base de données
        // DbSet permet d'effectuer des opérations CRUD (Create, Read, Update, Delete) sur les objets Eleve
        public DbSet<EleveAPI> Eleves { get; set; }
    
}
}
