using Microsoft.EntityFrameworkCore;
using Task7.Domain;

namespace Task7.Application.Interfaces;

public interface ITicTacToeDbContext
{
    DbSet<Player> Players { get; set; }
    DbSet<Game> Games { get; set; }
    DbSet<Group> Groups { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}