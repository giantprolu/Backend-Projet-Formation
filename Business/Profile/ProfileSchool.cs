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
        public static School ToSchool(this SchoolMini school, bool includeEleves = true)
        {
            return new School
            {
                Id = school.Id,
                Nom = school.Nom,
                Eleves = includeEleves ? school.Eleves?.Select(e => e.ToEleve(false)).ToList() : null
            };
        }

        public static SchoolMini ToSchoolMini(this School school, bool includeEleves = true)
        {
            return new SchoolMini
            {
                Id = school.Id,
                Nom = school.Nom,
                NmbEleve = school.Eleves?.Count ?? 0,
                Eleves = includeEleves ? school.Eleves?.Select(e => e.ToEleveMini(false)).ToList() : null
            };
        }
    }
}
