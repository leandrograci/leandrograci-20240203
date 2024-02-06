using RTE.GestaoUnidadesColaboradores.Application.DTO;
using RTE.GestaoUnidadesColaboradores.Domain.Entities;
using RTE.GestaoUnidadesColaboradores.Domain.Models.Unidade;
using RTE.GestaoUnidadesColaboradores.Service.Services;

namespace RTE.GestaoUnidadesColaboradores.Application.Applications;

public class UnidadeApplication
{
    private readonly IUnidadeService _service;

    public UnidadeApplication(IUnidadeService service)
    {
        _service = service;
    }

    public async Task<IEnumerable<GetUnidadesDTO>> GetUnidadesAsync()
    {
        var unidades =  await _service.GetUnidadesAsync();

        return unidades.Select(unidades => new GetUnidadesDTO
        {
            Id = unidades.Id,
            Nome = unidades.Nome,
            Codigo = unidades.Codigo,
            Status = unidades.Status,
            Colaboradores = unidades.Colaboradores.Select(colaboradores => new ColaboradorDTO
            {
                Id = colaboradores.Id,
                Nome = colaboradores.Nome
            })
        });
    }

    public async Task<NovaUnidadeViewModel> AddUnidadeAsync(NovaUnidadeViewModel model)
    {
        var unidade = new UnidadeEntity
        {
            Id = new Guid(),
            Nome = model.Nome,
            Codigo = model.Codigo,
            Status = true            
        };
        
        await _service.AddUnidadeAsync(unidade);

        model.Id = unidade.Id.ToString();       

        return model;
    }

    public async Task<UnidadeEntity> UpdateUnidadeAsync(AlterarUnidadeViewModel model)
    {
        try
        {
            var buscaUnidade = await _service.GetUnidadeByIdAsync(model.Id);

            if (buscaUnidade == null)
                throw new Exception("Unidade não encontrada!");

            buscaUnidade.Status = model.Ativo;

            return await _service.UpdateUnidadeAsync(buscaUnidade);
        }
        catch (Exception ex)
        {
            throw ex;
        }       
    }

}
