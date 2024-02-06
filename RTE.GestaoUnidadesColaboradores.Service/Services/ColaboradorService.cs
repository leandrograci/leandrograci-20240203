using RTE.GestaoUnidadesColaboradores.Domain.Entities;
using RTE.GestaoUnidadesColaboradores.Domain.Exceptions;
using RTE.GestaoUnidadesColaboradores.Infra.Interfaces.Colaborador;

namespace RTE.GestaoUnidadesColaboradores.Service.Services;

public interface IColaboradorService
{
    Task<IEnumerable<ColaboradorEntity>> GetColaboradoresAsync();
    Task<ColaboradorEntity> GetColaboradorByIdAsync(Guid colaboradorId);
    Task AddColaboradorAsync(ColaboradorEntity colaborador);
    Task<ColaboradorEntity> UpdateColaboradorAsync(ColaboradorEntity colaborador);
    Task DeleteColaboradorAsync(Guid colaboradorId);

}

public class ColaboradorService : IColaboradorService
{
    private readonly IColaboradorRepository _colaboradorRepository;
    private readonly IUnidadeService _unidadeService;

    public ColaboradorService(IColaboradorRepository colaboradorRepository, IUnidadeService unidadeService)
    {
        _colaboradorRepository = colaboradorRepository;
        _unidadeService = unidadeService;
    }

    public async Task<IEnumerable<ColaboradorEntity>> GetColaboradoresAsync()
    {
        return await _colaboradorRepository.GetColaboradoresAsync();
    }

    public async Task<ColaboradorEntity> GetColaboradorByIdAsync(Guid colaboradorId)
    {
        return await _colaboradorRepository.GetColaboradorByIdAsync(colaboradorId);
    }

    public async Task AddColaboradorAsync(ColaboradorEntity colaborador)
    {
        var unidade = _unidadeService.GetUnidadeByIdAsync(colaborador.UnidadeId).Result;

        if (!unidade.Status)
            throw new BusinessException("Não é possível cadastrar um colaborador em uma unidade inativa!");

        await _colaboradorRepository.AddAsync(colaborador);
    }

    public async Task<ColaboradorEntity> UpdateColaboradorAsync(ColaboradorEntity colaborador)
    {
        

        return await _colaboradorRepository.UpdateAsync(colaborador);
    }

    public async Task DeleteColaboradorAsync(Guid colaboradorId)
    {
        var buscaColaborador = await _colaboradorRepository.GetColaboradorByIdAsync(colaboradorId);
        if (buscaColaborador == null)
            throw new BusinessException("Colaborador não encontrado!");

        await _colaboradorRepository.DeleteAsync(buscaColaborador);
    }
}
