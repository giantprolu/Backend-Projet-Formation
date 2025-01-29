using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ModelMinimal
{
    public class SchoolMini
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public int NmbEleve { get; set; }
        public ICollection<EleveMini>? Eleves { get; set; }
    }
}