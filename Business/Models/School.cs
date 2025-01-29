using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class School
    {
        public int Id { get; set; }
        public required string Nom { get; set; }
        public List<Eleve>? Eleves { get; set; }
    }
}
