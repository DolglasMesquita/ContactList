using ContactList.Data;
using ContactList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactList.Services
{
    public class CategoriaService
    {
        private readonly ContactListContext _context;

        public CategoriaService(ContactListContext context)
        {
            _context = context;
        }

        public List<Categoria> FindAll()
        {
            return _context.Categorias.OrderBy(l => l.CategoriaNome).ToList();
        }
    }
}
