using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riok.Mapperly.Abstractions;

namespace Models.ModelMinimal
{
    public class SchoolMini
    {
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; }

        public ICollection<EleveMini>? Eleves { get; set; }
    }
}