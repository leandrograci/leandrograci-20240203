using Microsoft.EntityFrameworkCore;
using RTE.GestaoUnidadesColaboradores.Domain.Entities;

namespace RTE.GestaoUnidadesColaboradores.Infra.Interfaces.Colaborador
{
    public class ColaboradorRepository : IColaboradorRepository
    {
        private readonly RTEGestaoUnidadesColaboradoresDbContext _dbContext;
        public ColaboradorRepository(RTEGestaoUnidadesColaboradoresDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(ColaboradorEntity colaborador)
        {
            _dbContext.Colaboradores.Add(colaborador);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(ColaboradorEntity colaborador)
        {            
            _dbContext.Colaboradores.Remove(colaborador);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ColaboradorEntity> GetColaboradorByIdAsync(Guid colaboradorId)
        {
            return await _dbContext.Colaboradores.FirstOrDefaultAsync(colaborador => colaborador.Id == colaboradorId);
        }

        public async Task<IEnumerable<ColaboradorEntity>> GetColaboradoresAsync()
        {
            return await _dbContext.Colaboradores.Include(c=>c.Unidade).Include(c=>c.Usuario).ToListAsync();
        }

        public async Task<ColaboradorEntity> UpdateAsync(ColaboradorEntity colaborador)
        {
            _dbContext.Colaboradores.Update(colaborador);
            _dbContext.Entry(colaborador).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return colaborador;
        }
    }
}
