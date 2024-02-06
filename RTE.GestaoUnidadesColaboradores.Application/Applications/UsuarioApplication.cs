using RTE.GestaoUnidadesColaboradores.Domain.Entities;
using RTE.GestaoUnidadesColaboradores.Domain.Exceptions;
using RTE.GestaoUnidadesColaboradores.Domain.Models.Usuario;
using RTE.GestaoUnidadesColaboradores.Service.Services;

namespace RTE.GestaoUnidadesColaboradores.Application.Applications;

public class UsuarioApplication
{
    private readonly IUsuarioService _service;

    public UsuarioApplication(IUsuarioService service)
    {
        _service = service;
    }

    public async Task<IEnumerable<UsuarioEntity>> GetUsuariosAsync()
    {
        return await _service.GetUsuariosAsync();
    }

    public async Task<IEnumerable<UsuarioEntity>> GetUsuariosAtivosAsync()
    {
        return await _service.GetUsuariosAtivosAsync();
    }

    public async Task<NovoUsuarioViewModel> AddUsuarioAsync(NovoUsuarioViewModel model)
    {
        var usuario = new UsuarioEntity
        {
            Id = new Guid(),
            Nome = model.Email,
            Senha = model.Senha,
            Status = true
        };

        await _service.AddUsuarioAsync(usuario);

        model.Id = usuario.Id.ToString();
        model.Ativo = usuario.Status;

        return model;
    }

    public async Task<UsuarioEntity> UpdateUsuarioAsync(AlterarUsuarioViewModel model)
    {
        try
        {
            var buscaUsuario = await _service.GetUsuarioByIdAsync(model.Id);

            if (buscaUsuario == null)
                throw new BusinessException("Usuário não encontrado!");

            buscaUsuario.Senha = model.Senha;
            buscaUsuario.Status = model.Ativo;
           
            return await _service.UpdateUsuarioAsync(buscaUsuario);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}
