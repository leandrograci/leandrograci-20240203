using System.ComponentModel.DataAnnotations;

namespace RTE.GestaoUnidadesColaboradores.Domain.Models.Colaborador
{
    public class AlterarColaboradorViewModel
    {

        [Required(ErrorMessage = "O código do colaborador é obrigatório.")]
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public Guid UnidadeId { get; set; }
    }
}
