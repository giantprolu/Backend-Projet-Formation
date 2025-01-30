using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ModelMinimal;

namespace Business.Models
{
    public class School
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public int NmbEleve { get; set; }
    }
}