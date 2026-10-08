using Ensinly.Server.DTO;
using Ensinly.Server.Facade.Inteface;
using Microsoft.AspNetCore.Mvc;

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
            if(login.Email == null || login.Senha == null) {
                return BadRequest("Email e/ou senha inválido");
            }

            var usuario = await _facadeLogin.ValidarLogin(login);

            if (usuario == null) {
                return Unauthorized();
            }

            return Ok(usuario);
        }
    }
}
