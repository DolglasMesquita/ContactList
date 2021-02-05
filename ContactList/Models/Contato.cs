using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactList.Models
{
    public class Contato
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }

        [Display(Name ="Categoria")]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        

    }
}
