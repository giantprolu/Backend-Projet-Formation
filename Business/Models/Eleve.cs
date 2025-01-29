using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class Eleve
    {
        public int Id { get; set; }
        public required string Nom { get; set; }
        public required string Prenom { get; set; }
        public int Age { get; set; }
        public bool Sexe { get; set; }
        public int SchoolId { get; set; }
        public virtual School? Schools { get; set; }

    }
}
