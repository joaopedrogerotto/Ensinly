using Ensinly.Server.DTO;
using Ensinly.Server.Facade.Inteface;
using Ensinly.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ensinly.Server.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase {
        private readonly IFacadeAluno _facadeAluno;

        public AlunoController(IFacadeAluno facadeAluno) {
            _facadeAluno = facadeAluno;
        }


        [HttpPost]
        public async Task<IActionResult> CadastrarAluno([FromBody] AlunoCreateDTO dto) {
            try {
                var result = await _facadeAluno.CadastrarAluno(dto);
                if (result) {
                    return Ok();
                }
                return BadRequest();
            } catch (Exception ex) {
                return StatusCode(500, $"Erro ao cadastrar aluno: {ex.Message}");
            }
        }
    }
}
