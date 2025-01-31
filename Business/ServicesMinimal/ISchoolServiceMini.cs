using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ModelMinimal;
using Business.Models;
using Models.RepositorySchool;

namespace Business.ServicesMinimal
{
    public interface ISchoolServiceMini : ISchoolRepo<School>
    {
        new Task<List<School>> GetListSchoolsAsync();
    }
}