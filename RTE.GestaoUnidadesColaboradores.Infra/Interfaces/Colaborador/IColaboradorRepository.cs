using RTE.GestaoUnidadesColaboradores.Domain.Entities;

namespace RTE.GestaoUnidadesColaboradores.Infra.Interfaces.Colaborador;

public interface IColaboradorRepository
{
    Task<IEnumerable<ColaboradorEntity>> GetColaboradoresAsync();
    Task<ColaboradorEntity> GetColaboradorByIdAsync(Guid colaboradorId);
    Task AddAsync(ColaboradorEntity colaborador);
    Task<ColaboradorEntity> UpdateAsync(ColaboradorEntity colaborador);
    Task DeleteAsync(ColaboradorEntity colaborador);
}
