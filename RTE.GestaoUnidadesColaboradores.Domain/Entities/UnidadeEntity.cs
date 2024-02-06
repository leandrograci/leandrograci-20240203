using System.Text.Json.Serialization;

namespace RTE.GestaoUnidadesColaboradores.Domain.Entities
{
    public class UnidadeEntity : EntityBase
    {
        public string Codigo { get; set; }
        public bool Status { get; set; }

        [JsonIgnore]
        public List<ColaboradorEntity> Colaboradores { get; set; }
    }
}
