using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.ModelMinimal;

namespace Models.RepositorySchool
{
    internal class SchoolRepo : Repository<SchoolMini>, ISchoolRepo
    {
        private readonly EleveContextMini _context;

        public SchoolRepo(DbContextOptions<EleveContextMini> options)
        {
            _context = new EleveContextMini(options);
        }

        public async Task<List<SchoolMini>> GetListSchoolsAsync()
        {
            return await _context.Set<SchoolMini>().ToListAsync();
        }
    }
}