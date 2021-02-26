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

        [Required(ErrorMessage = "O nome precisa ter entre 3 e 20 caracteres")]
        [StringLength(20, MinimumLength = 3)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O número de telefone não pode ser vazio")]
        public string Telefone { get; set; }

        [Display(Name ="Categoria")]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public string UsuarioNome { get; set; }


    }
}
