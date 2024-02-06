using System.ComponentModel.DataAnnotations;

namespace RTE.GestaoUnidadesColaboradores.Domain.Entities
{
    public class EntityBase
    {        
        public Guid Id { get; set; }
        public string Nome { get; set; }
    }
}
