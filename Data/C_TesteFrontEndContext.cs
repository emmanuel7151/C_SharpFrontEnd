using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using C_TEste.Model;

namespace C_TesteFrontEnd.Data
{
    public class C_TesteFrontEndContext : DbContext
    {
        public C_TesteFrontEndContext (DbContextOptions<C_TesteFrontEndContext> options)
            : base(options)
        {
        }

        public DbSet<C_TEste.Model.Produto> Produto { get; set; }

        public DbSet<C_TEste.Model.Login> Login { get; set; }
    }
}
