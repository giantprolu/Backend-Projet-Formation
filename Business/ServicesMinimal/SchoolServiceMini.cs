using Business.Models;
using Business.Profile;
using Microsoft.EntityFrameworkCore;
using Models.ModelMinimal;
using Models.RepositorySchool;

namespace Business.ServicesMinimal
{
    internal class SchoolServiceMini : ISchoolServiceMini
    {
        private readonly ISchoolRepo<School> _schoolRepo;
        private readonly IEleveContextMini _context;

        public SchoolServiceMini(ISchoolRepo<School> schoolRepo, IEleveContextMini context)
        {
            _schoolRepo = schoolRepo;
            _context = context;
        }

        public Task<IEnumerable<SchoolMini>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<School>> GetListSchoolsAsync()
        {
            var schoolsMini = await _context.Schools.ToListAsync();
            return schoolsMini.Select(s => s.ToSchool()).ToList();
        }
    }
}