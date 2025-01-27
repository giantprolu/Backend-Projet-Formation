using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.ModelMinimal;

namespace Business.ServicesMinimal
{
    public class SchoolServiceMini : ISchoolServiceMini
    {
        private readonly EleveContextMini _context;

        public SchoolServiceMini(EleveContextMini context)
        {
            _context = context;
        }

        public async Task<List<SchoolMini>> GetListSchoolsAsync()
        {
            return await _context.Schools.ToListAsync();
        }
    }
}
