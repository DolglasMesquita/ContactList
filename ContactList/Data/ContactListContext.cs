using ContactList.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactList.Data
{
    public class ContactListContext : DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Contato> Contato { get; set; }

        public ContactListContext(DbContextOptions<ContactListContext> options) : base(options)
        {

        }
    }
}
