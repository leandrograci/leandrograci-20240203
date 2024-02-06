using RTE.GestaoUnidadesColaboradores.Domain.Entities;
using RTE.GestaoUnidadesColaboradores.Domain.Exceptions;
using RTE.GestaoUnidadesColaboradores.Infra.Interfaces.Unidade;

namespace RTE.GestaoUnidadesColaboradores.Service.Services;

public interface IUnidadeService
{
    Task<IEnumerable<UnidadeEntity>> GetUnidadesAsync();
    Task<UnidadeEntity> GetUnidadeByIdAsync(Guid unidadeId);
    Task AddUnidadeAsync(UnidadeEntity unidade);
    Task<UnidadeEntity> UpdateUnidadeAsync(UnidadeEntity unidade);
}
public class UnidadeService : IUnidadeService
{
    private readonly IUnidadeRepository _unidadeRepository;

    public UnidadeService(IUnidadeRepository unidadeRepository)
    {
        _unidadeRepository = unidadeRepository;
    }

    public async Task<IEnumerable<UnidadeEntity>> GetUnidadesAsync()
    {
        return await _unidadeRepository.GetUnidadesAsync();
    }

    public async Task<UnidadeEntity> GetUnidadeByIdAsync(Guid unidadeId)
    {
        return await _unidadeRepository.GetUnidadeByIdAsync(unidadeId);
    }

    public async Task AddUnidadeAsync(UnidadeEntity unidade)
    {
        var unidadeporcodigo = this.GetUnidadesAsync().Result.Where(x => x.Codigo == unidade.Codigo).FirstOrDefault();

        if (unidadeporcodigo != null)
            throw new BusinessException("Já existe uma unidade com este código!");

       await _unidadeRepository.AddAsync(unidade);
    }

    public async Task<UnidadeEntity> UpdateUnidadeAsync(UnidadeEntity unidade)
    {
        return await _unidadeRepository.UpdateAsync(unidade);
    }
}
