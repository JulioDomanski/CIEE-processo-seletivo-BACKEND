using CIEE_processo_seletivo_BACKEND.Models;
using Microsoft.EntityFrameworkCore;

namespace CIEE_processo_seletivo_BACKEND.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Curriculo> Curriculos { get; set; }
}