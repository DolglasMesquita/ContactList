using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactList.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "O nome da categoria não pode ser vazio")]
        public string CategoriaNome { get; set; }
        public IEnumerable<Contato> Contatos { get; set; }
        public string UsuarioNome { get; set; }
    }
}
