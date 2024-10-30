using Microsoft.EntityFrameworkCore;
using LikeButtonApp.Models;

public class LikeDbContext : DbContext
{
    public DbSet<LikeButton> LikeButtons { get; set; }

    public LikeDbContext(DbContextOptions<LikeDbContext> options) : base(options) { }
}
