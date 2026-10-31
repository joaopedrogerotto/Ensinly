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

        [HttpGet("{idUsuario}/alunos")]
        public async Task<IActionResult> GetAlunosByProfessor([FromRoute]int idUsuario) {
            if(idUsuario == 0 || idUsuario == null) {
                return BadRequest("ID do usuário inválido.");
            }

            try {
                var alunos = await _facadeProfessor.SelecionaAlunosPorProfessor(idUsuario);
                return Ok(alunos);
            } catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
