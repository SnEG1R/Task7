using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Task7.Application.Interfaces;
using Task7.Domain;

namespace Task7.Persistence.Contexts;

public sealed class TicTacToeDbContext : DbContext, ITicTacToeDbContext
{
    public DbSet<Player> Players { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Group> Groups { get; set; }

    public TicTacToeDbContext(DbContextOptions<TicTacToeDbContext> options) 
        : base(options)
    {
        Database.EnsureCreated();
    }
}