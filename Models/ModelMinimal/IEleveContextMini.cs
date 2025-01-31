using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Models.ModelMinimal
{
    public interface IEleveContextMini
    {
        DbSet<EleveMini> Eleves { get; set; }
        DbSet<SchoolMini> Schools { get; set; }

        EntityEntry<EleveMini> Entry(EleveMini eleveMini);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}