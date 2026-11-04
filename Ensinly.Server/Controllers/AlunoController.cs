using Ensinly.Server.DTO;
using Ensinly.Server.Exceptions;
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
        public async Task<IActionResult> PostAluno([FromBody] AlunoCreateDTO dto) {
            try {
                var result = await _facadeAluno.CadastrarAluno(dto);
                if (result) {
                    return Ok();
                }
                return BadRequest();
            } catch (EntityAlreadyExistsException ex) {
                return Conflict(new { message = ex.Message });
            } catch (Exception ex) {
                return StatusCode(500, $"Erro ao cadastrar aluno: {ex.Message}");
            }
        }

        [HttpGet("{emailAluno}")]
        public async Task<IActionResult> GetAlunoByEmail(string emailAluno) {
            if(String.IsNullOrEmpty(emailAluno)) {
                return BadRequest("Email do aluno é obrigatório.");
            }

            try {
                var aluno = await _facadeAluno.SelecionarPorEmail(emailAluno);
                return Ok(aluno);
            } catch (EntityNotFoundException ex) {
                return NotFound(new { message = ex.Message });
            } catch (Exception ex) {
                return StatusCode(500, $"Erro ao selecionar aluno por email: {ex.Message}");
            }
        }
    }
}