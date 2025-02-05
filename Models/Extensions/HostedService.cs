using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Models.ModelMinimal;

namespace Models.Extensions
{
    public class HostedService(IServiceProvider serviceProvider, IHostEnvironment env) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (env.IsDevelopment())
            {
                return;
            }
            using var scope = serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<EleveContextMini>();
            await db.Database.MigrateAsync(cancellationToken).ConfigureAwait(false);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}