using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ModelMinimal;

namespace Business.Models
{
    public class Eleve
    {
        public int Id { get; set; }

        // Propriété Nom de type chaîne de caractères, représente le nom de famille de l'élève
        public string Nom { get; set; }

        // Propriété Prenom de type chaîne de caractères, représente le prénom de l'élève
        public string Prenom { get; set; }

        // Propriété Age de type entier, représente l'âge de l'élève
        public int Age { get; set; }

        public bool Sexe { get; set; }

        // Propriété SchoolId pour la clé étrangère
        public int SchoolId { get; set; }
    }
}