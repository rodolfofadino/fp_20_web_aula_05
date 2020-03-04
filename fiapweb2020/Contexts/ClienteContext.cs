using fiapweb2020.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fiapweb2020.Contexts
{
    public class ClienteContext : DbContext
    {
        public ClienteContext(DbContextOptions<ClienteContext> options) : base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<Cliente>()
        //    //    .Property(a => a.Nome)
        //    //    .HasColumnName("NomeDoCliente")
        //    //    .HasColumnType("varchar");

        //}

    }
}
