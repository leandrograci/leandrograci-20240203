namespace RTE.GestaoUnidadesColaboradores.Domain.Models.Usuario
{
    public class AlterarUsuarioViewModel
    {

        public Guid Id { get; set; }

        public string Senha { get; set; }

        public bool Ativo { get; set; }
    }
}
