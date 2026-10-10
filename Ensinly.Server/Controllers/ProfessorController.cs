using Ensinly.Server.DTO;
using Ensinly.Server.Facade.Inteface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Ensinly.Server.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase {
        private readonly IFacadeProfessor _facadeProfessor;

        public ProfessorController(IFacadeProfessor facadeProfessor) {
            _facadeProfessor = facadeProfessor;
        }


        [HttpPost]
        public async Task<IActionResult> PostProfessor([FromBody] ProfessorCreateDTO professor) {
            try {
                var result = await _facadeProfessor.CadastrarProfessor(professor);
                if (result) {
                    return Ok();
                }
                return BadRequest();
            }catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
