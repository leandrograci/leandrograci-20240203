using System.ComponentModel.DataAnnotations;

namespace RTE.GestaoUnidadesColaboradores.Domain.Models.Colaborador
{
    public class NovoColaboradorViewModel
    {
        public string Id { get; set; } = "Não precisa informar!";

        [Required(ErrorMessage = "O nome do colaborador é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O código único do usuário é obrigatório.")]
        public Guid UsuarioId { get; set; }

        [Required(ErrorMessage = "O código único da unidade é obrigatório.")]
        public Guid UnidadeId { get; set; }
    }
}
