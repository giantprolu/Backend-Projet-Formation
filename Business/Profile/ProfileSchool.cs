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
        [MapperIgnoreSource(nameof(SchoolMini.NmbEleve))]
        [MapperIgnoreSource(nameof(SchoolMini.Eleves))]
        public static partial School ToSchool(this SchoolMini school);

        [MapperIgnoreTarget(nameof(SchoolMini.NmbEleve))]
        [MapperIgnoreTarget(nameof(SchoolMini.Eleves))]
        public static partial SchoolMini ToSchoolMini(this School school);
    }
}