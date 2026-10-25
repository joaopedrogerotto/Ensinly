using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ensinly.Server.Controllers {
    [ApiController]
    [Route("api")]
    public class AuthController : ControllerBase {
        [Authorize]
        [HttpGet("me")]
        public IActionResult Me() {
            return Ok(
                new {
                    id = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    nome = User.FindFirstValue(ClaimTypes.Name),
                    email = User.FindFirstValue(ClaimTypes.Email),
                    tipoUsuario = User.FindFirstValue(ClaimTypes.Role)
                }
            );
        }
    }
}
