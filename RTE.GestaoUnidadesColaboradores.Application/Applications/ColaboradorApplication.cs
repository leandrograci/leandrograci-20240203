using RTE.GestaoUnidadesColaboradores.Application.DTO;
using RTE.GestaoUnidadesColaboradores.Domain.Entities;
using RTE.GestaoUnidadesColaboradores.Domain.Exceptions;
using RTE.GestaoUnidadesColaboradores.Domain.Models.Colaborador;
using RTE.GestaoUnidadesColaboradores.Service.Services;

namespace RTE.GestaoUnidadesColaboradores.Application.Applications;

public class ColaboradorApplication
{
    private readonly IColaboradorService _service;
    private readonly IUnidadeService _unidadeService;

    public ColaboradorApplication(IColaboradorService service, IUnidadeService unidadeService)
    {
        _service = service;
        _unidadeService=unidadeService;
    }

    public async Task<IEnumerable<GetColaboradoresDTO>> GetColaboradores()
    {
        var colaboradores = await _service.GetColaboradoresAsync();

        return colaboradores.Select(colaborador => new GetColaboradoresDTO
        {
            Id = colaborador.Id,
            Nome = colaborador.Nome,
            Usuario = new UsuarioEntity
            {
                Id = colaborador.Usuario.Id,
                Nome = colaborador.Usuario.Nome,
                Senha = colaborador.Usuario.Senha,
                Status = colaborador.Usuario.Status
            },
            Unidade = new UnidadeEntity
            {
                Id = colaborador.Unidade.Id,
                Nome = colaborador.Unidade.Nome,
                Codigo = colaborador.Unidade.Codigo,
                Status = colaborador.Unidade.Status
            }
        });
    }

    public async Task<NovoColaboradorViewModel> AddColaboradorAsync(NovoColaboradorViewModel model)
    {
        var colaborador = new ColaboradorEntity
        {
            Id = new Guid(),
            Nome = model.Nome,
            UnidadeId = model.UnidadeId,
            UsuarioId = model.UsuarioId
        };
        
        await _service.AddColaboradorAsync(colaborador);

        model.Id = colaborador.Id.ToString();        

        return model;
    }

    public async Task<ColaboradorEntity> UpdateColaboradorAsync(AlterarColaboradorViewModel model)
    {
        try
        {
            var buscaColaborador = await _service.GetColaboradorByIdAsync(model.Id);
            if (buscaColaborador == null)
                throw new Exception("Colaborador não foi encontrado!");

            var unidade = _unidadeService.GetUnidadeByIdAsync(model.UnidadeId).Result;

            if (!unidade.Status && buscaColaborador.UnidadeId != model.UnidadeId)
                throw new BusinessException("Não é possível associar a uma unidade inativa!");

            buscaColaborador.Nome = model.Nome;
            buscaColaborador.UnidadeId = model.UnidadeId;

            return await _service.UpdateColaboradorAsync(buscaColaborador);
        }
        catch (Exception ex)
        {
            throw ex;
        }       
    }

    public async Task RemoverColaboradorAsync(Guid colaboradorId)
    {
        try
        {
            await _service.DeleteColaboradorAsync(colaboradorId);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}
