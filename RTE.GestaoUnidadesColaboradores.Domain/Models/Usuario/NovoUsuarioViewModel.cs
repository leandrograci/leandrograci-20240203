using System.ComponentModel.DataAnnotations;

namespace RTE.GestaoUnidadesColaboradores.Domain.Models.Usuario
{
    public class NovoUsuarioViewModel
    {
        public string Id { get; set; } = "Não precisa informar!";

        [Required(ErrorMessage = "O campo de Email é obrigatório.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo de Senha é obrigatório.")]
        public string Senha { get; set; }

        public bool Ativo { get; set; }
    }
}
