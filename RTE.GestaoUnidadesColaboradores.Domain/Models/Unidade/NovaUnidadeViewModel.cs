using System.ComponentModel.DataAnnotations;

namespace RTE.GestaoUnidadesColaboradores.Domain.Models.Unidade
{
    public class NovaUnidadeViewModel
    {
        public string Id { get; set; } = "Não precisa informar!";

        [Required(ErrorMessage = "O código da unidade é obrigatório.")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "O nome da unidade é obrigatório.")]
        public string Nome { get; set; }
 
    }
}
