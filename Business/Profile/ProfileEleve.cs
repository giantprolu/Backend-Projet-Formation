using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riok.Mapperly.Abstractions;
using Models.ModelMinimal;
using Business.Models;

namespace Business.Profile
{
    [Mapper]
    public static partial class ProfileEleve
    {
        public static Eleve ToEleve(this EleveMini eleve)
        {
            return new Eleve
            {
                Id = eleve.Id,
                Nom = eleve.Nom,
                Prenom = eleve.Prenom,
                Age = eleve.Age,
                Sexe = eleve.Sexe,
                SchoolId = eleve.SchoolId,
                Schools = eleve.Schools?.ToSchool()
            };
        }
    }
}
