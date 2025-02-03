using Business.Models;
using Business.Profile;
using Microsoft.EntityFrameworkCore;
using Models.ModelMinimal;
using Models.RepositorySchool;

namespace Business.ServicesMinimal
{
    internal class SchoolServiceMini : ISchoolServiceMini
    {
        private readonly ISchoolRepo _schoolRepo;

        public SchoolServiceMini(ISchoolRepo schoolRepo)
        {
            _schoolRepo = schoolRepo;
        }

        public async Task<List<School>> GetListSchoolsAsync()
        {
            var schoolsMini = await _schoolRepo.GetListSchoolsAsync();
            return schoolsMini.Select(s => s.ToSchool()).ToList();
        }
    }
}