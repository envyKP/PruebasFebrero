using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Login> Login { get; set; }
    public DbSet<Tarea> Tareas { get; set; }
} 