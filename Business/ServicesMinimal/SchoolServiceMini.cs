using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.ModelMinimal;
using Business.Profile;
using Business.Models;

namespace Business.ServicesMinimal
{
    internal class SchoolServiceMini : ISchoolServiceMini
    {
        private readonly EleveContextMini _context;

        public SchoolServiceMini(EleveContextMini context)
        {
            _context = context;
        }

        public async Task<List<School>> GetListSchoolsAsync()
        {
            var schoolsMini = await _context.Schools.ToListAsync();
            return schoolsMini.Select(s => s.ToSchool()).ToList();
        }
    }
}