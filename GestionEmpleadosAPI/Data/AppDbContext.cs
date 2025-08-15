using GestionEmpleadosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionEmpleadosAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Empleado> Empleados { get; set; }

    }
}
