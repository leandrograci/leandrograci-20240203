using RTE.GestaoUnidadesColaboradores.Domain.Entities;

namespace RTE.GestaoUnidadesColaboradores.Application.DTO
{
    public class GetColaboradoresDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public UsuarioEntity Usuario { get; set; }
        public UnidadeEntity Unidade { get; set; }
    }
}
