using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RTE.GestaoUnidadesColaboradores.Application.Applications;
using RTE.GestaoUnidadesColaboradores.Domain.Models.Colaborador;
using Swashbuckle.AspNetCore.Annotations;


[ApiController]
[Route("api/[controller]/[action]")]
public class ColaboradorController : Controller
{
    private readonly ColaboradorApplication _colaboradorApplication;

    public ColaboradorController(ColaboradorApplication colaboradorApplication)
    {
        _colaboradorApplication = colaboradorApplication;
    }

    [HttpGet]
    [Authorize(Policy = "BearerPolicy")]
    public async Task<IActionResult> BuscaColaboradores()
    {
        var response = await _colaboradorApplication.GetColaboradores();

        return Ok(response);
    }


    [HttpPost]
    [Authorize(Policy = "BearerPolicy")]
    [SwaggerOperation(Summary = "Cria um novo colaborador", Description = "Cria um novo colaborador com base nos dados fornecidos.")]
    [SwaggerResponse(201, "Criado com sucesso", typeof(NovoColaboradorViewModel))]
    [SwaggerResponse(400, "Requisição inválida", typeof(void))]
    public async Task<IActionResult> CriarColaborador(NovoColaboradorViewModel model)
    {
        if (ModelState.IsValid)        
            await _colaboradorApplication.AddColaboradorAsync(model);        

        return Ok(model);
    }
  
    [HttpPost]
    [Authorize(Policy = "BearerPolicy")]
    [SwaggerOperation(Summary = "Atualiza um colaborador existente", Description = "Atualiza os detalhes de um colaborador existente com base no ID.")]
    [SwaggerResponse(204, "Atualizado com sucesso", typeof(NovoColaboradorViewModel))]
    [SwaggerResponse(400, "Requisição inválida", typeof(void))]
    [SwaggerResponse(404, "Não encontrado", typeof(void))]
    public async Task<IActionResult> AlterarColaborador(AlterarColaboradorViewModel model)
    {
        if (ModelState.IsValid)
            await _colaboradorApplication.UpdateColaboradorAsync(model);

        return Ok(model);
    }

    [HttpDelete]
    [Authorize(Policy = "BearerPolicy")]
    [SwaggerOperation(Summary = "Atualiza um colaborador existente", Description = "Atualiza os detalhes de um colaborador existente com base no ID.")]
    [SwaggerResponse(204, "Atualizado com sucesso", typeof(NovoColaboradorViewModel))]
    [SwaggerResponse(400, "Requisição inválida", typeof(void))]
    [SwaggerResponse(404, "Não encontrado", typeof(void))]
    public async Task<IActionResult> RemoverColaborador(Guid colaboradorId)
    {
        if (ModelState.IsValid)
            await _colaboradorApplication.RemoverColaboradorAsync(colaboradorId);

        return Ok("Colaborador removido com sucesso!");
    }
}