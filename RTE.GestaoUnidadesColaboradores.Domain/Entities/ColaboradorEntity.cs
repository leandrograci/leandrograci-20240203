using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RTE.GestaoUnidadesColaboradores.Domain.Entities
{
    public class ColaboradorEntity : EntityBase
    {        
        public Guid UnidadeId { get; set; }
        public Guid UsuarioId { get; set; }        

        [ForeignKey("UsuarioId")]
        [JsonIgnore]
        public UsuarioEntity Usuario { get; set; }

        [ForeignKey("UnidadeId")]
        [JsonIgnore]
        public UnidadeEntity Unidade { get; set; }
    }
}
