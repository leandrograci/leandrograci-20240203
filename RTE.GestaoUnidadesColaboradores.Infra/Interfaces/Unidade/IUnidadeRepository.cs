using RTE.GestaoUnidadesColaboradores.Domain.Entities;

namespace RTE.GestaoUnidadesColaboradores.Infra.Interfaces.Unidade;

public interface IUnidadeRepository
{
    Task<IEnumerable<UnidadeEntity>> GetUnidadesAsync();
    Task<UnidadeEntity> GetUnidadeByIdAsync(Guid unidadeId);    
    Task AddAsync(UnidadeEntity unidade);
    Task<UnidadeEntity> UpdateAsync(UnidadeEntity unidade);
}
