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
        [MapperIgnoreSource(nameof(EleveMini.Schools))]
        public static partial Eleve ToEleve(this EleveMini eleve);

        [MapperIgnoreTarget(nameof(EleveMini.Schools))]
        public static partial EleveMini ToEleveMini(this Eleve eleve);
    }
}