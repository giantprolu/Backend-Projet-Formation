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
    public static partial class ProfileSchool
    {
        public static School ToSchool(this SchoolMini school)
        {
            return new School
            {
                Id = school.Id,
                Nom = school.Nom,
                Eleves = school.Eleves?.Select(e => e.ToEleve()).ToList() ?? new List<Eleve>()
            };
        }
    }
}
