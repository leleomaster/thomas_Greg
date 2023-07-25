using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ThomasGreg.Domain.Models;

namespace ThomasGreg.Web.Data
{
    public class ThomasGregWebContext : DbContext
    {
        public ThomasGregWebContext (DbContextOptions<ThomasGregWebContext> options)
            : base(options)
        {
        }

        public DbSet<ThomasGreg.Domain.Models.ClienteViewModel> ClienteViewModel { get; set; } = default!;
    }
}
