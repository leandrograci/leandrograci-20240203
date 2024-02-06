using RTE.GestaoUnidadesColaboradores.Domain.Entities;
using RTE.GestaoUnidadesColaboradores.Infra.Interfaces.Usuario;

namespace RTE.GestaoUnidadesColaboradores.Service.Services;


public interface IUsuarioService
{
    Task<IEnumerable<UsuarioEntity>> GetUsuariosAsync();

    Task<IEnumerable<UsuarioEntity>> GetUsuariosAtivosAsync();

    Task<UsuarioEntity> GetUsuarioByIdAsync(Guid usuarioId);

    Task AddUsuarioAsync(UsuarioEntity usuario);

    Task<UsuarioEntity> UpdateUsuarioAsync(UsuarioEntity usuario);
}

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<IEnumerable<UsuarioEntity>> GetUsuariosAsync()
    {
        return await _usuarioRepository.GetUsuariosAsync();
    }

    public async Task<IEnumerable<UsuarioEntity>> GetUsuariosAtivosAsync()
    {
        return _usuarioRepository.GetUsuariosAsync().Result.Where(usuario=>usuario.Status);
    }

    public async Task<UsuarioEntity> GetUsuarioByIdAsync(Guid usuarioId)
    {
        return await _usuarioRepository.GetUsuarioByIdAsync(usuarioId);
    }

    public async Task AddUsuarioAsync(UsuarioEntity usuario)
    {
        _usuarioRepository.AddAsync(usuario);
    }

    public async Task<UsuarioEntity> UpdateUsuarioAsync(UsuarioEntity usuario)
    {
        return await _usuarioRepository.UpdateAsync(usuario);
    }
}
