using Ensinly.Server.Exceptions;
using Ensinly.Server.Facade.Inteface;
using Ensinly.Server.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ensinly.Server.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase {
        private readonly IFacadeToken _facadeToken;

        public TokenController(IFacadeToken facadeToken) {
            _facadeToken = facadeToken;
        }

        [HttpPost("validar")]
        public async Task<IActionResult> ValidateToken([FromBody] string token) {
            if (string.IsNullOrEmpty(token)) {
                return BadRequest("É necessário informar um token");
            }

            try {
                var usuario = await _facadeToken.ValidarToken(token);
                if (usuario == null) {
                    return Unauthorized();
                }

                var claims = new List<Claim> {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.Role, usuario.TipoUsuario)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


                return Ok(usuario);
            } catch (InvalidOrExpiredTokenException ieEx) {
                return Unauthorized(ieEx.Message);

            } catch (Exception ex) {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
