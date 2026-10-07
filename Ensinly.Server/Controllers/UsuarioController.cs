using Ensinly.Server.DTO;
using Ensinly.Server.Facade.Inteface;
using Ensinly.Server.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace Ensinly.Server.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase {
        private readonly IFacadeUsuario _facadeUsuario;

        public UsuarioController(IFacadeUsuario facadeUsuario) {
            _facadeUsuario = facadeUsuario;
        }

        [HttpPost]
        public IActionResult PostUsuario([FromBody] UsuarioCreateDTO UsuarioCreateDTO) {
            try {
                var usuario = UsuarioMapper.ToEntidade(UsuarioCreateDTO);
                _facadeUsuario.CadastrarUsuario(usuario);
                return Ok("Usuario cadastrado com sucesso!");
            } catch (Exception ex) {
                return BadRequest($"Erro ao cadastrar usuario: {ex.Message}");
            }
        }
    }
}
