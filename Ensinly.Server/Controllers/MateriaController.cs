using Ensinly.Server.Facade.Inteface;
using Microsoft.AspNetCore.Mvc;

namespace Ensinly.Server.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class MateriaController : ControllerBase {
        private readonly IFacadeMateria _facadeMateria;

        public MateriaController(IFacadeMateria facadeMateria) {
            _facadeMateria = facadeMateria;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var materias = await _facadeMateria.SelecionaTodas();
            return Ok(materias);
        }
    }
}
