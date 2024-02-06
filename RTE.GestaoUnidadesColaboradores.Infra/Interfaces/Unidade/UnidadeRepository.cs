using Microsoft.EntityFrameworkCore;
using RTE.GestaoUnidadesColaboradores.Domain.Entities;

namespace RTE.GestaoUnidadesColaboradores.Infra.Interfaces.Unidade
{
    public class UnidadeRepository : IUnidadeRepository
    {
        private readonly RTEGestaoUnidadesColaboradoresDbContext _dbContext;

        public UnidadeRepository(RTEGestaoUnidadesColaboradoresDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(UnidadeEntity unidade)
        {
            _dbContext.Unidades.Add(unidade);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<UnidadeEntity> GetUnidadeByIdAsync(Guid unidadeId)
        {
            return await _dbContext.Unidades.FirstOrDefaultAsync(unidade => unidade.Id == unidadeId);
        }

        public async Task<IEnumerable<UnidadeEntity>> GetUnidadesAsync()
        {
            return await _dbContext.Unidades.Include(u=>u.Colaboradores).ToListAsync();
        }

        public async Task<UnidadeEntity> UpdateAsync(UnidadeEntity unidade)
        {
            _dbContext.Unidades.Update(unidade);
            _dbContext.Entry(unidade).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return unidade;
        }
    }
}
