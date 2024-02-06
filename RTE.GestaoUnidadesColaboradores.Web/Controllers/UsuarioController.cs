using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RTE.GestaoUnidadesColaboradores.Application.Applications;
using RTE.GestaoUnidadesColaboradores.Domain.Models.Usuario;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


[ApiController]
[Route("api/[controller]/[action]")]
public class UsuarioController : Controller
{
    private readonly UsuarioApplication _usuarioApplication;

    public UsuarioController(UsuarioApplication usuarioApplication)
    {
        _usuarioApplication = usuarioApplication;
       
    }

    [HttpGet]
    public string GerarTokenAcesso()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
     
        var key = Encoding.UTF8.GetBytes("qFBfvoaGAaXMPtqUON63xBiVF9EiSLIEZk14CJszAe0");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "RTEUser"),
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);

    }

    [HttpGet]
    [Authorize(Policy = "BearerPolicy")]
    public async Task<IActionResult> BuscaUsuariosAsync()
    {
        var response = await _usuarioApplication.GetUsuariosAsync();

        return Ok(response);
    }

    [HttpGet]
    [Authorize(Policy = "BearerPolicy")]
    public async Task<IActionResult> BuscaUsuariosAtivosAsync()
    {
        var response = await _usuarioApplication.GetUsuariosAtivosAsync();

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "BearerPolicy")]
    [SwaggerOperation(Summary = "Cria um novo usuário", Description = "Cria um novo usuário com base nos dados fornecidos.")]
    [SwaggerResponse(201, "Criado com sucesso", typeof(NovoUsuarioViewModel))]
    [SwaggerResponse(400, "Requisição inválida", typeof(void))]
    public async Task<IActionResult> CriarUsuarioAsync(NovoUsuarioViewModel model)
    {
        if (ModelState.IsValid)        
            await _usuarioApplication.AddUsuarioAsync(model);        

        return Ok(model);
    }
  
    [HttpPost]
    [Authorize(Policy = "BearerPolicy")]
    [SwaggerOperation(Summary = "Atualiza um usuário existente", Description = "Atualiza os detalhes de um usuário existente com base no ID.")]
    [SwaggerResponse(204, "Atualizado com sucesso", typeof(NovoUsuarioViewModel))]
    [SwaggerResponse(400, "Requisição inválida", typeof(void))]
    [SwaggerResponse(404, "Não encontrado", typeof(void))]
    public async Task<IActionResult> AlterarUsuarioAsync(AlterarUsuarioViewModel model)
    {
        if (ModelState.IsValid)
            await _usuarioApplication.UpdateUsuarioAsync(model);

        return Ok(model);
    }
}