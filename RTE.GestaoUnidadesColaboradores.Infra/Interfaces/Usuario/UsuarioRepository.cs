using Microsoft.EntityFrameworkCore;
using RTE.GestaoUnidadesColaboradores.Domain.Entities;

namespace RTE.GestaoUnidadesColaboradores.Infra.Interfaces.Usuario
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly RTEGestaoUnidadesColaboradoresDbContext _dbContext;

        public UsuarioRepository(RTEGestaoUnidadesColaboradoresDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(UsuarioEntity usuario)
        {
            _dbContext.Usuarios.Add(usuario);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<UsuarioEntity>> GetUsuariosAsync()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }

        public async Task<UsuarioEntity> GetUsuarioByIdAsync(Guid usuarioId)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(usuario => usuario.Id == usuarioId);
        }
     
        public async Task<UsuarioEntity> UpdateAsync(UsuarioEntity usuario)
        {            
            _dbContext.Usuarios.Update(usuario);
            _dbContext.Entry(usuario).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return usuario;
        }
    }
}
