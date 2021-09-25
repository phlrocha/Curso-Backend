using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CursoBackEnd.Models.Usuarios
{
    public class RegistroViewModelInput
    {
        [Required(ErrorMessage = "Login é obrigatório")]
        public string Login { get; set; }

        [Required(ErrorMessage = "E-mail é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório")]
        public string Senha { get; set; }
    }
}
