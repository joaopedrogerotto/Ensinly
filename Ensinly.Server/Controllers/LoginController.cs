using Ensinly.Server.DTO;
using Ensinly.Server.Facade.Inteface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ensinly.Server.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController: ControllerBase {
        private readonly IFacadeLogin _facadeLogin;

        public LoginController(IFacadeLogin facadeLogin) {
            _facadeLogin = facadeLogin; 
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO login) {
            try {
                if (login.Email == null || login.Senha == null) {
                    return BadRequest("Email e/ou senha inválido.");
                }

                var usuario = await _facadeLogin.ValidarLogin(login);

                if (usuario == null) {
                    return Unauthorized("Falha ao realizar login.");
                }

                if (string.IsNullOrEmpty(usuario.Email)) {
                    return Unauthorized("Email e/ou senha inválido.");
                }

                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.Email, usuario.Email)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return Ok(usuario);

            } catch (Exception ex) {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
}
