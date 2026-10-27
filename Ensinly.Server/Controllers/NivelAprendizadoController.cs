using Ensinly.Server.Facade.Inteface;
using Microsoft.AspNetCore.Mvc;

namespace Ensinly.Server.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class NivelAprendizadoController : ControllerBase {
        private readonly IFacadeNivelAprendizado _facadeNivelAprendizado;

        public NivelAprendizadoController(IFacadeNivelAprendizado facadeNivelAprendizado) {
            _facadeNivelAprendizado = facadeNivelAprendizado;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            try {
                var niveis = await _facadeNivelAprendizado.SelecionarTodos();
                return Ok(niveis);
            }catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
