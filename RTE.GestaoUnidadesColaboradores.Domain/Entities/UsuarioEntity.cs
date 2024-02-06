using System.Text.Json.Serialization;

namespace RTE.GestaoUnidadesColaboradores.Domain.Entities;

public class UsuarioEntity : EntityBase
{
    public string Senha { get; set; }
    public bool Status { get; set; }

    [JsonIgnore]
    public ColaboradorEntity Colaborador { get; set; }
}
