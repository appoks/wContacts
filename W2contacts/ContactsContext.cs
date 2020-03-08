using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using W2contacts.Models;

namespace W2contacts
{
    public class ContactsContext : DbContext
    {
     

        public ContactsContext(DbContextOptions<ContactsContext> options) : base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Concessionaria> Concessionarias { get; set; }

        /* CONSOLE APP
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                @"Server=localhost;Port=5432;Database=WAY2-TST;User Id=postgres;Password=postgres;");
        }
        */
    }


}
