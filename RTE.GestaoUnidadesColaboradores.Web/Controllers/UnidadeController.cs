using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RTE.GestaoUnidadesColaboradores.Application.Applications;
using RTE.GestaoUnidadesColaboradores.Domain.Models.Unidade;
using Swashbuckle.AspNetCore.Annotations;


[ApiController]
[Route("api/[controller]/[action]")]
public class UnidadeController : Controller
{
    private readonly UnidadeApplication _unidadeApplication;

    public UnidadeController(UnidadeApplication unidadeApplication)
    {
        _unidadeApplication = unidadeApplication;
    }

    [HttpGet]
    [Authorize(Policy = "BearerPolicy")]
    public async Task<IActionResult> BuscaUnidades()
    {
        var response = await _unidadeApplication.GetUnidadesAsync();

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "BearerPolicy")]
    [SwaggerOperation(Summary = "Cria uma nova unidade", Description = "Cria uma nova unidade com base nos dados fornecidos.")]
    [SwaggerResponse(201, "Criado com sucesso", typeof(NovaUnidadeViewModel))]
    [SwaggerResponse(400, "Requisição inválida", typeof(void))]
    public async Task<IActionResult> CriarUnidade(NovaUnidadeViewModel model)
    {
        if (ModelState.IsValid)        
            await _unidadeApplication.AddUnidadeAsync(model);        

        return Ok(model);
    }
  
    [HttpPost]
    [Authorize(Policy = "BearerPolicy")]
    [SwaggerOperation(Summary = "Atualiza uma unidade existente", Description = "Atualiza os detalhes de uma unidade existente com base no ID.")]
    [SwaggerResponse(204, "Atualizado com sucesso", typeof(NovaUnidadeViewModel))]
    [SwaggerResponse(400, "Requisição inválida", typeof(void))]
    [SwaggerResponse(404, "Não encontrada", typeof(void))]
    public async Task<IActionResult> AlterarUnidadeAsync(AlterarUnidadeViewModel model)
    {
        if (ModelState.IsValid)
            await _unidadeApplication.UpdateUnidadeAsync(model);

        return Ok(model);
    }
   
}