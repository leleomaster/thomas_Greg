using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ThomasGreg.Domain.Entities;

namespace ThomasGreg.Infrastructure.Contexts
{
    public class SqlServerContext : IdentityDbContext<ApplicationUser>
    {
        public SqlServerContext() { }

        public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Logradouro> Logradouros { get; set; }

    }
}

