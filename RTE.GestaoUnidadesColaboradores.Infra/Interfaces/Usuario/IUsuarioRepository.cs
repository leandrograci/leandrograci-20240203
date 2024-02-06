using RTE.GestaoUnidadesColaboradores.Domain.Entities;

namespace RTE.GestaoUnidadesColaboradores.Infra.Interfaces.Usuario;

public interface IUsuarioRepository
{
    Task<IEnumerable<UsuarioEntity>> GetUsuariosAsync();
    Task<UsuarioEntity> GetUsuarioByIdAsync(Guid usuarioId);
    Task AddAsync(UsuarioEntity usuario);
    Task<UsuarioEntity> UpdateAsync(UsuarioEntity usuario);
}
