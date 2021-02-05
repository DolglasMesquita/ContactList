using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactList.Models.ViewModels
{
    public class ContatosFormViewModel
    {
        public Contato Contato { get; set; }
        public ICollection<Categoria> Categorias { get; set; }
    }
}
