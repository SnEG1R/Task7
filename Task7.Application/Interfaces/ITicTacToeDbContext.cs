using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Task7.Domain;

namespace Task7.Application.Interfaces;

public interface ITicTacToeDbContext
{
    DbSet<Player> Players { get; set; }
    DbSet<Game> Games { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}