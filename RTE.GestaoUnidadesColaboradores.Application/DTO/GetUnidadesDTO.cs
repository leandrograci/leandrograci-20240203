using RTE.GestaoUnidadesColaboradores.Domain.Entities;

namespace RTE.GestaoUnidadesColaboradores.Application.DTO
{
    public class GetUnidadesDTO
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public bool Status { get; set; }

        public IEnumerable<ColaboradorDTO> Colaboradores { get; set; }
    }
}
